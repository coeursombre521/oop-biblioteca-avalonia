using PAOO.Biblioteca.Models;
using PAOO.Biblioteca.Visitors;

namespace PAOO.Biblioteca.Interfaces
{
    public interface IBorrowable
    {
        bool MarkAsBorrowed(Membru membru);
        bool MarkAsBorrowedByUserHavingId(Guid guid);
        void Accept(IVisitorItem visitor);
    }
}