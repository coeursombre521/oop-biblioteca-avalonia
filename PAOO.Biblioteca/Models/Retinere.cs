namespace PAOO.Biblioteca.Models
{
    public class Retinere: IComparable<Guid>
    {
        public Guid Id { get; private set; }
        public DateTime DataLimita { get; set; }
    
        public BorrowableItem BorrowableItem { get; set; }
        public Membru Membru { get; set; }

        public Retinere(DateTime dataLimita, BorrowableItem borrowableItem, Membru membru)
        {
            Id = Guid.NewGuid();
            DataLimita = dataLimita;
            BorrowableItem = borrowableItem;
            Membru = membru;
        }

        public int CompareTo(Guid other)
        {
            return Id.CompareTo(other);
        }

        public int DaysOverdue()
        {
            return (DateTime.Now - DataLimita).Days;
        }
    }
}
