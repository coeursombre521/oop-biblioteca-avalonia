using PAOO.Biblioteca.Collections;
using PAOO.Biblioteca.Factories;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca
{
    public class BibliotecaContext
    {
        private const double PENALTY_BASE_PRICE = 1.0;

        private CarteFactory carteFactory = new();
        private RevistaFactory revistaFactory = new();

        private BorrowableItemCatalog borrowableItems = BorrowableItemCatalog.GetInstance();
        private MembriCatalog membri = MembriCatalog.GetInstance();
        private RetineriCatalog retineri = RetineriCatalog.GetInstance();

        private TaxCalculator taxCalculator = new(CalculateTaxStrategy.GetInstance(), PENALTY_BASE_PRICE);

        private static BibliotecaContext? _instance;

        private BibliotecaContext() {}

        public static BibliotecaContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new();
            }

            return _instance;
        }
        
        public void AddItem(BorrowableItemProperties properties)
        {
            switch (properties)
            {
                case CarteProperties carteProperties:
                    borrowableItems.Add(carteFactory.CreateItem(carteProperties));
                    Logger.AddLogItem(carteProperties.Id, $"Cartea {carteProperties.Titlu} a fost adaugata cu succes!");
                    break;
                case RevistaProperties revistaProperties:
                    borrowableItems.Add(revistaFactory.CreateItem(revistaProperties));
                    Logger.AddLogItem(revistaProperties.Id, $"Revista {revistaProperties.Titlu} a fost adaugata cu succes!");
                    break;
            }
        }

        public void RemoveItem(Guid id)
        {
            borrowableItems.Remove(id);
            Logger.AddLogItem(id, "Itemul a fost sters cu succes!");
        }

        public BorrowableItem? GetItemById(Guid id)
        {
            return borrowableItems.GetOneByIdentifier(id);
        }

        public void AddMember(string nume, string adresa, string telefon)
        {
            Logger.AddLogMembru(nume, $"Este adaugat membrul cu adresa {adresa} si telefonul {telefon}");
            membri.Add(new Membru(nume, adresa, telefon));
        }

        public void RemoveMember(Guid id)
        {
            var membru = membri.GetOneByIdentifier(id);

            if (membru != null)
            {
                if (membru.BorrowedItems.Count > 0)
                {
                    Logger.AddLogMembru(membru.Nume, $"Membrul inca are carti de returnat. Nu se va sterge membrul.");
                    return;
                }
                Logger.AddLogMembru(membru.Nume, $"Se sterge membrul");
            }
            retineri.RemoveByMemberId(id);
            membri.Remove(id);
        }

        public List<Membru> GetMembers()
        {
            return membri.GetAll();
        }

        public Membru? GetMemberById(Guid id)
        {
            return membri.GetOneByIdentifier(id);
        }

        public bool BorrowItem(Guid itemId, Guid memberId)
        {
            BorrowableItem? item = borrowableItems.GetOneByIdentifier(itemId);
            Membru? membru = membri.GetOneByIdentifier(memberId);

            if (item == null || membru == null)
            {
                return false;
            }
            else
            {
                Logger.AddLogMembru(membru.Nume, $"Itemul cu titlul {item.Titlu} a fost imprumutat de un membru");
                Logger.AddLogItem(item.Id, $"Itemul este marcat ca imprumutat de {membru.Nume}. Data limita este {item.DataLimita}");
                return membru.BorrowItem(item);
            }
        }

        public bool ReturnItem(Guid itemId, Guid memberId)
        {
            BorrowableItem? item = borrowableItems.GetOneByIdentifier(itemId);
            Membru? membru = membri.GetOneByIdentifier(memberId);

            if (item == null || membru == null)
            {
                return false;
            }
            else
            {
                List<Retinere> retineriByItem = retineri.GetByItemId(item.Id);
                bool hasRetineri = retineriByItem.Count > 0;

                SetTaxStrategyByRetineri(hasRetineri);

                membru.AddPenalty(ReturnTaxByDaysOverdue(item));

                Logger.AddLogMembru(membru.Nume, $"Itemul cu titlul {item.Titlu} a fost returnat de un membru");
                Logger.AddLogItem(item.Id, $"Itemul este marcat ca returnat de {membru.Nume}");

                bool result = membru.ReturnItem(item);

                if (hasRetineri) {
                    var retinere = retineriByItem.First();
                    BorrowItem(retinere.BorrowableItem.Id, retinere.Membru.Id);
                    RemoveRetinere(retinere.Id);
                }

                return result;
            }
        }

        public int DaysItemReturnOverdue(Guid itemId, Guid memberId)
        {
            BorrowableItem? item = borrowableItems.GetOneByIdentifier(itemId);
            Membru? membru = membri.GetOneByIdentifier(memberId);

            if (item == null || membru == null)
            {
                return 0;
            }
            else
            {
                return item.DaysOverdue();
            }
        }

        public int DaysRetinereOverdue(Guid retinereId)
        {
            Retinere? retinere = retineri.GetOneByIdentifier(retinereId);

            if (retinere == null)
            {
                return 0;
            }
            else
            {
                return retinere.DaysOverdue();
            }
        }

        public void PayPenalty(Guid membruId)
        {
            Membru? membru = membri.GetOneByIdentifier(membruId);

            if (membru != null)
            {
                membru.PayPenalty();
                Logger.AddLogMembru(membru.Nume, $"Se plateste penalizarea");
            }
        }

        public int CountItems() => borrowableItems.Count;

        public int CountMembers() => membri.Count;

        public List<BorrowableItem> GetBorrowedItemsByMember(Guid memberId)
        {
            Membru? membru = membri.GetOneByIdentifier(memberId);

            if (membru == null)
            {
                return new List<BorrowableItem>();
            }
            else
            {
                Logger.AddLogMembru(membru.Nume, $"Sunt {membru.BorrowedItems.Count} elemente imprumutate de membru.");
                return membru.BorrowedItems;
            }
        }

        public List<BorrowableItem> GetBorrowedItems()
        {
            return borrowableItems.GetAll().FindAll(x => x.Membru != null);
        }

        public List<BorrowableItem> GetAvailableItems()
        {
            return borrowableItems.GetAll().FindAll(x => x.Membru == null);
        }

        public List<BorrowableItem> GetItems()
        {
            return borrowableItems.GetAll();
        }

        public void AddRetinere(Guid itemId, Guid membruId)
        {
            BorrowableItem? item = borrowableItems.GetOneByIdentifier(itemId);
            Membru? membru = membri.GetOneByIdentifier(membruId);

            if (item == null || membru == null)
            {
                return;
            }
            else
            {
                Logger.AddLogMembru(membru.Nume, $"Itemul cu titlul {item.Titlu} a fost retinut de un membru");
                Logger.AddLogItem(item.Id, $"Itemul este marcat ca retinut de {membru.Nume}");

                retineri.Add(new Retinere(DateTime.Now.AddDays(14), item, membru));
            }
        }

        public void RemoveRetinere(Guid id)
        {
            Logger.AddLogRetinere(id, $"Se sterge retinerea");
            retineri.Remove(id);
        }

        public Retinere? GetRetinereById(Guid id)
        {
            return retineri.GetOneByIdentifier(id);
        }
        
        public int CountRetineri()
        {
            return retineri.Count;
        }

        public List<Retinere> GetRetineri()
        {
            return retineri.GetAll();
        }

        public List<Retinere> GetRetineriByMember(Guid membruId)
        {
            return retineri.GetAll().FindAll(x => x.Membru.Id == membruId);
        }

        public List<Retinere> GetRetineriByItem(Guid itemId)
        {
            return retineri.GetAll().FindAll(x => x.BorrowableItem.Id == itemId);
        }

        public void PrintLog()
        {
            Logger.Instance.PrintLogs();
        }

        public List<Log> GetLogs()
        {
            return Logger.Instance.GetAllLogs();
        }

        public void ClearLog()
        {
            Logger.Instance.ClearLogs();
        }

        private void SetTaxStrategyByRetineri(bool hasRetineri)
        {
            if (hasRetineri)
            {
                taxCalculator.SetStrategy(CalculateTaxWithDoublePenaltyStrategy.GetInstance());
                Logger.AddLogMembru("Membru", $"Cartea selectata ar putea avea retineri");
            }
            else
            {
                taxCalculator.SetStrategy(CalculateTaxStrategy.GetInstance());
            }
        }

        private double ReturnTaxByDaysOverdue(BorrowableItem item)
        {
            var result = (item.DaysOverdue() > 0) ? taxCalculator.CalculateTax(item.DaysOverdue()) : 0.0;

            Logger.AddLogItem(item.Id, $"Penalizare pentru returnare intarziata: {result}");
            return result;
        }
    }
}