using System;

namespace PAOO.Main.Models;

public class Log
{
    public string? Message { get; set; }
    public LogType Type { get; set; }
    public DateTime Timestamp { get; set; }

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