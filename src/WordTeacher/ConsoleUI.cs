// See https://aka.ms/new-console-template for more information

internal enum ConsoleAlign
{
    Left,
    Center,
    Right,
}

internal class ConsoleUI
{
    private static int WindowWidth => Console.WindowWidth;
    private static int WindowHeight => Console.WindowHeight;
    

    public static ConsoleAlign DefaultAlign = ConsoleAlign.Left;

    public static void Write(string text, int left, int top, ConsoleColor? color = null)
    {
        Console.SetCursorPosition(left, top);
        Write(text, color);
    }

    public void WriteLine(string text, int left, int top, ConsoleColor? color = null)
    {
        Write(text, left, top, color);
        Console.WriteLine();
    }

    public static void Write(string text, ConsoleAlign? align, ConsoleColor? color = null)
    {
        var left = GetLeftFromAlign(text, align ?? DefaultAlign);
        Write($"{text.PadLeft(left + text.Length)}", color);
    }

    public static void WriteLine(string text, ConsoleAlign? align, ConsoleColor? color = null)
    {
        Write(text, align, color);
        Console.WriteLine();
    }

    public static void Write(string text, ConsoleColor? color = null)
    {
        if (color == null)
        {
            Console.Write(text);
            return;
        }

        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = color.Value;
        Console.Write(text);
        Console.ForegroundColor = oldColor;
    }

    private static int GetLeftFromAlign(string text, ConsoleAlign align) => align switch
    {
        ConsoleAlign.Center => WindowWidth / 2 - text.Length / 2,
        ConsoleAlign.Right => WindowWidth - text.Length,
        _ => 0
    };
}
