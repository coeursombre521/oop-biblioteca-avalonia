using PAOO.Biblioteca.Builders;
using PAOO.Biblioteca.Factories;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Factories
{
    public abstract class BaseBorrowableItemFactory : IBorrowableItemFactory
    {
        protected readonly ItemBuilder itemBuilder = new();

        public BorrowableItem? CreateItem(BorrowableItemProperties properties)
        {
            return CreateSpecificItem(properties);
        }

        protected abstract BorrowableItem? CreateSpecificItem(BorrowableItemProperties properties);

        protected void ApplyDecoratorsIfNecessary(BorrowableItemProperties properties)
        {
            if (properties.InSala != null)
            {
                itemBuilder.WithItemSala(properties.InSala.Value);
            }

            if (properties.Taxa != null)
            {
                itemBuilder.WithItemTaxa(properties.Taxa.Value);
            }
        }
    }
}

