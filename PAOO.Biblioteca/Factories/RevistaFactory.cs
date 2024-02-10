using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Factories
{
    public class RevistaFactory : BaseBorrowableItemFactory
    {
        protected override BorrowableItem? CreateSpecificItem(BorrowableItemProperties properties)
        {
            if (properties is RevistaProperties revistaProperties)
            {
                itemBuilder.CreateRevista(revistaProperties.Id, revistaProperties.Titlu);

                ApplyDecoratorsIfNecessary(properties);

                return itemBuilder.Build();
            }
            else
            {
                return null;
            }
        }
    }
}