using System;
using System.Collections.Generic;
using PAOO.Main.Models;
using PAOO.Biblioteca;
using PAOO.Main.ModelAdapters;

namespace PAOO.Main.Services;

public class LogService : BaseService<Biblioteca.Models.Log, Log>
{
    private LogAdapter _logAdapter = new();

    public List<Log> GetLog() => ConvertList(BibliotecaContext.GetInstance().GetLogs(), _logAdapter);

    public void ClearLog() => BibliotecaContext.GetInstance().ClearLog();
}
