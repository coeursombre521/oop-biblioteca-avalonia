using System;
using PAOO.Main.Models;

namespace PAOO.Main.ModelAdapters;

class BorrowableItemAdapter : IModelAdapter<Biblioteca.Models.BorrowableItem, BorrowableItem>
{
    private CarteAdapter _carteAdapter = new CarteAdapter();
    private RevistaAdapter _revistaAdapter = new RevistaAdapter();
    
    public BorrowableItem? ConvertToModel(Biblioteca.Models.BorrowableItem? obj, bool includes = false)
    {
        if (obj == null)
        {
            return null;
        }

        switch (obj)
        {
            case Biblioteca.Models.Carte carte:
                return _carteAdapter.ConvertToModel(carte, includes);
            case Biblioteca.Models.Revista revista:
                return _revistaAdapter.ConvertToModel(revista, includes);
            default:
                return null;
        }
    }
}