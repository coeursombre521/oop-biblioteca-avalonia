using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Collections
{
    public class BorrowableItemCatalog : BaseCatalog<BorrowableItem, Guid>
    {
        private static BorrowableItemCatalog? _instance;

        private BorrowableItemCatalog() { }

        public static BorrowableItemCatalog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new();
            }
            return _instance;
        }

        public void Add(BorrowableItem? item) => base.Add(item);

        public bool Remove(Guid id) => base.Remove(id);

        public BorrowableItem? GetOneByIdentifier(Guid id) => base.GetOneByIdentifier(id);

        public int Count => base.Count;
    }
}
