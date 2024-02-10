using PAOO.Biblioteca.Collections;
using PAOO.Biblioteca.Interfaces;
using PAOO.Biblioteca.Visitors;

namespace PAOO.Biblioteca.Models
{
    public class Carte: BorrowableItem
    {
        public string Autor { get; set; }

        public Carte(Guid id, string titlu, string autor) : base(id, titlu)
        {
            Autor = autor;
        }

        public new void Accept(IVisitorItem visitor)
        {
            visitor.Visit(this);
        }
    }
}
