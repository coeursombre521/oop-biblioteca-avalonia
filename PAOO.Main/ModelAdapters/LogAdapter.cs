using PAOO.Main.Models;

namespace PAOO.Main.ModelAdapters;

public class LogAdapter : IModelAdapter<PAOO.Biblioteca.Models.Log, Log>
{
    public Log? ConvertToModel(Biblioteca.Models.Log? obj, bool includes = false)
    {
        if (obj == null)
        {
            return null;
        }
        
        return new Log
        {
            Message = obj.Message,
            Timestamp = obj.Timestamp,
            Type = ConvertBibliotecaTypeToLogType(obj.Type)
        };
    }

    private Log.LogType ConvertBibliotecaTypeToLogType(Biblioteca.Models.Log.LogType type)
    {
        switch (type)
        {
            case Biblioteca.Models.Log.LogType.Membru:
                return Log.LogType.Membru;
            case Biblioteca.Models.Log.LogType.Item:
                return Log.LogType.Item;
            case Biblioteca.Models.Log.LogType.Retinere:
                return Log.LogType.Retinere;
            default:
                return Log.LogType.Membru;
        }
    }
}