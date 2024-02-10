using System.Collections.Generic;
using PAOO.Main.Models;

namespace PAOO.Main.ModelAdapters;

public class MembruAdapter : IModelAdapter<Biblioteca.Models.Membru, Membru>
{
    private BorrowableItemAdapter _borrowableItemAdapter = new();

    public Membru? ConvertToModel(Biblioteca.Models.Membru? obj, bool includes = false)
    {
        if (obj == null)
        {
            return null;
        }
        
        return new Membru
        {
            Id = obj.Id,
            Nume = obj.Nume,
            Adresa = obj.Adresa,
            Telefon = obj.Telefon,
            Penalizare = obj.Penalizare,
            BorrowedItems = includes ? GetBorrowedItems(obj) : []
        };
    }

    private List<BorrowableItem> GetBorrowedItems(Biblioteca.Models.Membru obj)
    {
        List<BorrowableItem> borrowedItems = [];
        foreach (var i in obj.BorrowedItems)
        {
            var borrowedItem = _borrowableItemAdapter.ConvertToModel(i, false);
            if (borrowedItem != null)
            {
                borrowedItems.Add(borrowedItem);
            }
        }
        return borrowedItems; 
    }
}