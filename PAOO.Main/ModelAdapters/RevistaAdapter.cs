using PAOO.Main.Models;

namespace PAOO.Main.ModelAdapters;

public class RevistaAdapter : IModelAdapter<PAOO.Biblioteca.Models.Revista, Revista>
{
    public Revista? ConvertToModel(Biblioteca.Models.Revista? obj, bool includes = false)
    {
        if (obj == null)
        {
            return null;
        }
        
        return new Revista
        {
            Id = obj.Id,
            Titlu = obj.Titlu,
            DataLimita = obj.DataLimita,
            Membru = includes ? ConvertMembru(obj.Membru) : null
        };
    }
    
    private Membru? ConvertMembru(Biblioteca.Models.Membru? membru)
    {
        if (membru == null)
        {
            return null;
        }

        return new Membru
        {
            Id = membru.Id,
            Nume = membru.Nume,
            Adresa = membru.Adresa,
            Telefon = membru.Telefon,
            Penalizare = membru.Penalizare,
            BorrowedItems = []
        };
    }
}