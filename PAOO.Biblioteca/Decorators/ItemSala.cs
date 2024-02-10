using PAOO.Biblioteca.Interfaces;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Decorators
{
    public class ItemSala : BaseDecorator, IBorrowable, IReturnable
    {
        public bool InSala { get; set; }

        public ItemSala(BorrowableItem item, bool inSala) : base(item)
        {
            InSala = inSala;
        }
    }
}