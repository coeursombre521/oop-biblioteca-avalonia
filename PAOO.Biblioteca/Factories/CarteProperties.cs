namespace PAOO.Biblioteca.Factories
{
    public class CarteProperties : BorrowableItemProperties
    {
        public string Autor { get; set; }

        public CarteProperties(string titlu, string autor) : base(titlu)
        {
            Autor = autor;
        }

        public CarteProperties(string titlu, string autor, bool inSala) : base(titlu, inSala)
        {
            Autor = autor;
        }

        public CarteProperties(string titlu, string autor, bool inSala, double taxa) : base(titlu, inSala, taxa)
        {
            Autor = autor;
        }
    }
}