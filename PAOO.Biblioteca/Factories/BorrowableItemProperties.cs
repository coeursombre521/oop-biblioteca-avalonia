namespace PAOO.Biblioteca.Factories
{
    public abstract class BorrowableItemProperties
    {
        public Guid Id { get; set; }
        public string Titlu { get; set; }
        public bool? InSala { get; set; }
        public double? Taxa { get; set; }

        public BorrowableItemProperties(string titlu)
        {
            Id = Guid.NewGuid();
            Titlu = titlu;
        }

        public BorrowableItemProperties(string titlu, bool inSala)
        {
            Id = Guid.NewGuid();
            Titlu = titlu;
            InSala = inSala;
        }

        public BorrowableItemProperties(string titlu, bool inSala, double taxa)
        {
            Id = Guid.NewGuid();
            Titlu = titlu;
            InSala = inSala;
            Taxa = taxa;
        }
    }
}