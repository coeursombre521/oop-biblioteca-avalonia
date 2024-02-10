using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Factories
{
    public class CarteFactory : BaseBorrowableItemFactory
    {
        protected override BorrowableItem? CreateSpecificItem(BorrowableItemProperties properties)
        {
            if (properties is CarteProperties carteProperties)
            {
                itemBuilder.CreateCarte(carteProperties.Id, carteProperties.Titlu, carteProperties.Autor);

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