using System;
using System.Collections.Generic;

namespace PAOO.Main.Models;

public class Membru : IComparable<Guid>
{
    public Guid Id { get; set; }
    public string? Nume { get; set; }
    public string? Adresa { get; set; }
    public string? Telefon { get; set; }
    public double Penalizare { get; set; }

    public List<BorrowableItem> BorrowedItems { get; set; } = new List<BorrowableItem>();
    
    public int CompareTo(Guid other)
    {
        return Id.CompareTo(other);
    }
}