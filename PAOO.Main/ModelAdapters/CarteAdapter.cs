using PAOO.Main.Models;

namespace PAOO.Main.ModelAdapters;

public class CarteAdapter : IModelAdapter<Biblioteca.Models.Carte, Carte>
{
    public Carte? ConvertToModel(Biblioteca.Models.Carte? obj, bool includes = false)
    {
        if (obj == null)
        {
            return null;
        }

        return new Carte
        {
            Id = obj.Id,
            Titlu = obj.Titlu,
            DataLimita = obj.DataLimita,
            Autor = obj.Autor,
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