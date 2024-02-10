using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca
{
    public class Logger
    {
        private readonly List<Log> _logs = new List<Log>();
        private static readonly Logger _instance = new Logger();

        private Logger() { }

        public static Logger Instance => _instance;

        public static void AddLog(string message, Log.LogType type)
        {
            var logEntry = new Log(message, type);
            Instance._logs.Add(logEntry);
        }

        public static void AddLogMembru(string numeMembru, string message)
        {
            var logEntry = new Log($"(Nume membru: {numeMembru}) " + message, Log.LogType.Membru);
            Instance._logs.Add(logEntry);
        }

        public static void AddLogItem(Guid itemId, string message)
        {
            var logEntry = new Log($"(ID item: {itemId}) " + message, Log.LogType.Item);
            Instance._logs.Add(logEntry);
        }

        public static void AddLogRetinere(Guid retinereId, string message)
        {
            var logEntry = new Log($"(ID retinere: {retinereId}) " + message, Log.LogType.Retinere);
            Instance._logs.Add(logEntry);
        }

        public List<Log> GetAllLogs()
        {
            return _logs;
        }

        public void ClearLogs()
        {
            _logs.Clear();
        }

        public void PrintLogs()
        {
            foreach (var log in _logs)
            {
                Console.WriteLine(log.ToString());
            }
        }
    }
}