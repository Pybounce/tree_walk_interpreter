

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
        Run(source);
        if (_hadError) { Environment.Exit(0); }
    }

    static void Run(string source)
    {
        if (source == null) { throw new NullReferenceException("source code cannot be null"); }
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        var parser = new Parser(tokens);
        var expression = parser.Parse();

        if (_hadError || expression == null) { return; }

        Console.WriteLine(new AstPrinter().Print(expression));
    }

    public static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    public static void Error(Token token, string message)
    {
        if (token.TokenType == TokenType.EOF)
        {
            Report(token.Line, " at end", message);
        }
        else
        {
            Report(token.Line, $"at '{token.Lexeme}'", message);
        }
    }
    
    private static void Report(int line, string where, string message)
    {
        Console.WriteLine($"Line {line}, Error {where}: {message}");
        _hadError = true;
    }
}




