using PAOO.Biblioteca.Interfaces;
using PAOO.Biblioteca.Models;
using PAOO.Biblioteca.Visitors;

namespace PAOO.Biblioteca.Decorators
{
    public abstract class BaseDecorator : BorrowableItem, IBorrowable, IReturnable
    {
        protected BorrowableItem _item;

        public BaseDecorator(BorrowableItem item) : base(item.Id, item.Titlu)
        {
            _item = item;
        }

        public void SetItem(BorrowableItem item)
        {
            _item = item;
        }

        public new bool MarkAsBorrowed(Membru membru) => _item.MarkAsBorrowed(membru);

        public new bool MarkAsBorrowedByUserHavingId(Guid guid) => _item.MarkAsBorrowedByUserHavingId(guid);
       
        public new void Accept(IVisitorItem visitor) => _item.Accept(visitor);

        public new bool MarkAsReturned() => _item.MarkAsReturned();
    }
}