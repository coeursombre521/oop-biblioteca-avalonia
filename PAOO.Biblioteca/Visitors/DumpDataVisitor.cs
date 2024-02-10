using System;
using PAOO.Biblioteca.Decorators;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Visitors
{
    public class DumpDataVisitor : IVisitorItem
    {
        public void Visit(BorrowableItem item)
        {
            Console.WriteLine($"(BorrowableItem) {item.Titlu}: id={item.Id}");
        }

        public void Visit(Carte carte)
        {
            Console.WriteLine($"(Carte) {carte.Titlu}: id={carte.Id}, autor={carte.Autor}");
        }

        public void Visit(Revista revista)
        {
            Console.WriteLine($"(Revista) {revista.Titlu}: id={revista.Id}");
        }

        public void Visit(ItemSala itemSala)
        {
            string yesOrNo = itemSala.InSala ? "da" : "nu";
            Console.Write($"(Doar in sala: {yesOrNo}) ");
        }

        public void Visit(ItemTaxa itemTaxa)
        {
            Console.Write($"(Element taxabil: {itemTaxa.Taxa}) ");
        }
    }
}