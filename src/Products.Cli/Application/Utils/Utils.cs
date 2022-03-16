namespace Products.Cli.Application.Utils;
public class Utils
{
    public static void WriteLine(string message, ConsoleColor color, bool readKey = false)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);

        if (readKey)
            Console.ReadKey();
    }
}

