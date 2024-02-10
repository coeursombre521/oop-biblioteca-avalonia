using PAOO.Biblioteca.Interfaces;
using PAOO.Biblioteca.Visitors;

namespace PAOO.Biblioteca.Models
{
    public abstract class BorrowableItem : IComparable<Guid>, IBorrowable, IReturnable
    {
        public Guid Id { get; set; }
        public string Titlu { get; set; }
        public DateTime? DataLimita { get; private set; }
        
        public Membru? Membru { get; private set; }

        protected BorrowableItem(Guid id, string titlu)
        {
            Id = id;
            Titlu = titlu;
        }
        
        public int CompareTo(Guid other)
        {
            return Id.CompareTo(other);
        }

        public bool MarkAsBorrowed(Membru membru)
        {
            Membru = membru;
            DataLimita = DateTime.Now.AddDays(14);
            return true;
        }

        public bool MarkAsBorrowedByUserHavingId(Guid guid)
        {
            Membru? membru = BibliotecaContext.GetInstance().GetMemberById(guid);
            if (membru == null)
            {
                return false;
            }
            else
            {
                return MarkAsBorrowed(membru);
            }
        }

        public bool MarkAsReturned()
        {
            Membru = null;
            DataLimita = null;
            return true;
        }

        public int DaysOverdue()
        {
            return DataLimita == null ? 0 : (DateTime.Now - DataLimita.Value).Days;
        }

        public void Accept(IVisitorItem visitor)
        {
            visitor.Visit(this);
        }
    }
}