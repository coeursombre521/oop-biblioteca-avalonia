using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Factories
{
    interface IBorrowableItemFactory
    {
        BorrowableItem? CreateItem(BorrowableItemProperties properties);
    }
}