using PAOO.Biblioteca.Collections;

namespace PAOO.Biblioteca.Models
{
    public class Membru: IComparable<Guid>
    {
        public Guid Id { get; private set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public double Penalizare { get; private set; }

        public List<BorrowableItem> BorrowedItems { get; set; } = new();

        public Membru(string nume, string adresa, string telefon) {
            Id = Guid.NewGuid();
            Nume = nume;
            Adresa = adresa;
            Telefon = telefon;
            Penalizare = 0.0;
        }

        public int CompareTo(Guid other)
        {
            return Id.CompareTo(other);
        }

        public bool BorrowItem(BorrowableItem item)
        {
            item.MarkAsBorrowed(this);
            BorrowedItems.Add(item);
            return true;
        }

        public bool ReturnItem(BorrowableItem item)
        {
            item.MarkAsReturned();
            BorrowedItems.Remove(item);
            return true;
        }

        public bool BorrowItemHavingId(Guid guid)
        {
            BorrowableItem? item = BibliotecaContext.GetInstance().GetItemById(guid);
            if (item == null)
            {
                return false;
            }
            else
            {
                return BorrowItem(item);
            }
        }

        public bool ReturnItemHavingId(Guid guid)
        {
            BorrowableItem? item = BorrowedItems.Find(x => x.Id == guid);
            if (item == null)
            {
                return false;
            }
            else
            {
                return ReturnItem(item);
            }
        }

        public void ReturnEverything()
        {
            foreach (var item in BorrowedItems)
            {
                item.MarkAsReturned();
            }
            BorrowedItems.Clear();
        }

        public void AddPenalty(double tax)
        {
            Penalizare += tax;
        }

        public void PayPenalty()
        {
            Penalizare = 0.0;
        }
    }
}
