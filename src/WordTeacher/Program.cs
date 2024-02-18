// See https://aka.ms/new-console-template for more information

Console.Clear();

ConsoleUI.WriteLine("Witaj! Gotowy?", ConsoleAlign.Center, ConsoleColor.Green);
Console.ReadKey();

var width = Console.WindowWidth;
var height = Console.WindowHeight;
ConsoleUI.WriteLine($"{width} {height}", ConsoleAlign.Center);

var collection = ReadFile(Path.Combine(Environment.CurrentDirectory, "dane.txt"));
var round = new Round(collection);

Entry? current;
while ((current = round.GetNext()) != null)
{
    Console.Clear();
    ConsoleUI.Write($"{current.Question}: ", 10, 2);
    var answer = Console.ReadLine() ?? string.Empty;
    var top = 3;
    var left = 10;
    if (round.ValidateAnswer(current, answer))
        ConsoleUI.Write("Dobrze!", left, top, ConsoleColor.Green);
    else
    {
        ConsoleUI.Write($"Niestety nie. Poprawna odpowiedź: ", left, top);
        ConsoleUI.Write(current.Answer, ConsoleColor.Cyan);
    }

    Console.ReadKey();
}

var (good, percentage) = round.GetStatistics();
var bad = collection.Entries.Length - good;

Console.Clear();
ConsoleUI.WriteLine("Koniec", ConsoleAlign.Center, ConsoleColor.Cyan);
ConsoleUI.WriteLine($"Odpowiedzi dobrych: {good}, złych: {bad}, procent: {percentage}", ConsoleAlign.Center);


Collection ReadFile(string path)
{
    if (!File.Exists(path))
    {
        Console.WriteLine(Environment.CurrentDirectory);
        throw new FileNotFoundException($"Plik {path} nie istnieje");
    }

    var lines = File.ReadAllLines(path);
    if (!lines[0].StartsWith("#"))
        throw new FormatException("Pierwsza linia musi zawierać #nazwa1=nazwa2");

    var header = ReadEntry(lines[0].TrimStart('#'));
    var entries = ReadEntries(lines.Skip(1));

    return new Collection(header.Question, header.Answer, entries.ToArray());
}

IEnumerable<Entry> ReadEntries(IEnumerable<string> lines)
{
    return lines.Select(ReadEntry);
}

Entry ReadEntry(string line)
{
    var parts = line.Split('=');
    if (parts.Length != 2)
        throw new Exception($"Niewłaściwy wpis '{line}'");

    return new Entry(parts[0].Trim(), parts[1].Trim());
}


internal record Entry(string Question, string Answer);
internal record Collection(string Values1Description, string Values2Description, Entry[] Entries);