using PAOO.Biblioteca.Interfaces;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Decorators
{
    public class ItemTaxa : BaseDecorator, IBorrowable, IReturnable
    {
        public double Taxa { get; set; }

        public ItemTaxa(BorrowableItem item, double taxa) : base(item)
        {
            Taxa = taxa;
        }
    }
}