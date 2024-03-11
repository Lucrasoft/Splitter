namespace Tester;

public class Logger
{
    public static bool enabled = false;
    public static void Log(string? message)
    {
        if (!enabled) return;
        Console.WriteLine(message);
    }
}
