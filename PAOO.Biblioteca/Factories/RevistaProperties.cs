namespace PAOO.Biblioteca.Factories
{
    public class RevistaProperties : BorrowableItemProperties
    {
        public RevistaProperties(string titlu) : base(titlu) {}
        
        public RevistaProperties(string titlu, bool inSala) : base(titlu, inSala) {}
        
        public RevistaProperties(string titlu, bool inSala, double taxa) : base(titlu, inSala, taxa) {}
    }
}