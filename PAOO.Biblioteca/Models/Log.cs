namespace PAOO.Biblioteca.Models
{
    public class Log
    {
        public string Message { get; private set; }
        public LogType Type { get; private set; }
        public DateTime Timestamp { get; private set; } = DateTime.Now;

        public Log(string message, LogType type)
        {
            Message = message;
            Type = type;
        }

        public override string ToString()
        {
            return $"[{Type}, {Timestamp}] {Message}";
        }

        public enum LogType
        {
            Membru,
            Item,
            Retinere
        }
    }
}
