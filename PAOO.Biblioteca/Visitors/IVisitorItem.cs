using PAOO.Biblioteca.Decorators;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Visitors
{
    public interface IVisitorItem
    {
        void Visit(BorrowableItem item);
        void Visit(Carte carte);
        void Visit(Revista revista);
        void Visit(ItemSala itemSala);
        void Visit(ItemTaxa itemTaxa);
    }
}