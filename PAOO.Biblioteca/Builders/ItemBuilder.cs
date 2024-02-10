
using PAOO.Biblioteca.Decorators;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Builders
{
    public class ItemBuilder
    {
        private BorrowableItem? _item;

        public ItemBuilder()
        {
            Reset();            
        }

        public void Reset()
        {
            _item = null;
        }

        public ItemBuilder CreateRevista(Guid id, string name)
        {
            if (_item != null)
            {
                throw new Exception("Item already created");
            }

            _item = new Revista(id, name);
            return this;
        }

        public ItemBuilder CreateCarte(Guid id, string name, string autor)
        {
            if (_item != null)
            {
                throw new Exception("Item already created");
            }
            
            _item = new Carte(id, name, autor);
            return this;
        }

        public ItemBuilder WithItemSala(bool inSala)
        {
            if (_item == null)
            {
                throw new Exception("Item not created");
            }

            BorrowableItem enclosingItem = _item;

            _item = new ItemSala(enclosingItem, inSala);

            return this;
        }

        public ItemBuilder WithItemTaxa(double taxa)
        {
            if (_item == null)
            {
                throw new Exception("Item not created");
            }

            BorrowableItem enclosingItem = _item;

            _item = new ItemTaxa(enclosingItem, taxa);

            return this;
        }

        public BorrowableItem Build()
        {
            if (_item == null)
            {
                throw new Exception("Item not created");
            }

            BorrowableItem result = _item;

            Reset();

            return result;
        }
    }
}

