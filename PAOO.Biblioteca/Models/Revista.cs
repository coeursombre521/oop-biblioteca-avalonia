using PAOO.Biblioteca.Visitors;

namespace PAOO.Biblioteca.Models
{
    public class Revista: BorrowableItem
    {
        public Revista(Guid id, string titlu) : base(id, titlu) {}
        
        public new void Accept(IVisitorItem visitor)
        {
            visitor.Visit(this);
        }
    }
}