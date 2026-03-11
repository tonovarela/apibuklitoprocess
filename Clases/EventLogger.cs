using System;
using System.Text.Json;

namespace apiBukLitoprocess.Clases;

public static class EventLogger
{
 public static void Info(string eventName, object? data = null) =>
        Write("INFO", eventName, data, ConsoleColor.Cyan);

    public static void Success(string eventName, object? data = null) =>
        Write("OK", eventName, data, ConsoleColor.Green);

    public static void Warning(string eventName, object? data = null) =>
        Write("WARN", eventName, data, ConsoleColor.Yellow);

    public static void Error(string eventName, Exception ex, object? data = null) =>
        Write("ERROR", eventName, new
        {
            message = ex.Message,
            detail = ex.GetBaseException().Message,
            data
        }, ConsoleColor.Red);

    private static void Write(string level, string eventName, object? payload, ConsoleColor color)
    {
        var previous = Console.ForegroundColor;
        Console.ForegroundColor = color;


        string body;
        try
        {
            body = payload is null ? "-" : JsonSerializer.Serialize(payload);
        }
        catch
        {
            body = "No se pudo serializar el payload";
        }

        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {eventName} | {body}");
        Console.ForegroundColor = previous;
    }
}
