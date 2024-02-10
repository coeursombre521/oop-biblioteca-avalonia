using System;

namespace PAOO.Main.Models;

public class Retinere : IComparable<Guid>
{
    public Guid Id { get; set; }
    public DateTime DataLimita { get; set; }

    public BorrowableItem? BorrowableItem { get; set; }
    public Membru? Membru { get; set; }

    public int CompareTo(Guid other)
    {
        return Id.CompareTo(other);
    }
}
