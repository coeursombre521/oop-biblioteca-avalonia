using System;
using System.Collections.Generic;

namespace PAOO.Main.Models;

public abstract class BorrowableItem : IComparable<Guid>
{
    public Guid Id { get; set; }
    public string? Titlu { get; set; }
    public DateTime? DataLimita { get; set; }
    
    public Membru? Membru { get; set; }

    public string TypeName => GetType().Name;

    public int CompareTo(Guid other)
    {
        return Id.CompareTo(other);
    }
}