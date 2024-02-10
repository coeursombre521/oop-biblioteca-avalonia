using System;
using PAOO.Main.ModelAdapters;

namespace PAOO.Main.Models;

public class RetinereAdapter : IModelAdapter<Biblioteca.Models.Retinere, Retinere>
{
    private MembruAdapter _membruAdapter = new();
    private BorrowableItemAdapter _borrowableItemAdapter = new();

    public Retinere? ConvertToModel(Biblioteca.Models.Retinere? obj, bool includes = true)
    {
        if (obj == null)
        {
            return null;
        }
        
        return new Retinere
        {
            Id = obj.Id,
            DataLimita = obj.DataLimita,
            Membru = includes ? _membruAdapter.ConvertToModel(obj.Membru, false) : null,
            BorrowableItem = includes ? _borrowableItemAdapter.ConvertToModel(obj.BorrowableItem, false) : null
        };
    }
}