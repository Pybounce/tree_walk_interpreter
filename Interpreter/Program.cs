

using System.Net;

class Lox
{

    private static bool _hadError = false;

    static void Main(string[] args)
    {
        if (args.Length == 1)
        {
            RunFile(args[0]);
        }
        else
        {
            throw new ArgumentException("Must have exactly one argument");
        }
    }



    static void RunFile(string path)
    {
        var source = System.IO.File.ReadAllText(path);
        run(source);
        if (_hadError) { Environment.Exit(0); }
    }

    static void run(string source)
    {
        if (source == null) { throw new NullReferenceException("source code cannot be null"); }
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        foreach (Token token in tokens)
        {
            Console.WriteLine(token.ToString());
        }
    }

    public static void Error(int line, string message)
    {
        report(line, "", message);
    }
    
    private static void report(int line, string where, string message)
    {
        Console.WriteLine($"Line {line}, Error {where}: {message}");
        _hadError = true;
    }
}



