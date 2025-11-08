

class Lox
{

    private static bool _hadError = false;
    private static bool _hadRuntimeError = false;
    private static Interpreter _interpreter = new Interpreter();

    static void Main(string[] args)
    {

        if (args.Length == 1) { RunFile(args[0]); }
        else if (args.Length == 0) { RunPrompt(); }
        else
        {
            throw new ArgumentException("Must have less than 2 arguments");
        }
    }

    static void RunPrompt()
    {
        while (true)
        {
            Console.Write(">");
            var input = Console.ReadLine();
            if (input == null) { break; }
            Run(input);
            Console.WriteLine();
            _hadError = false;
        }

    }

    static void RunFile(string path)
    {
        var source = System.IO.File.ReadAllText(path);
        Run(source);
        if (_hadError) { Environment.Exit(65); }
        if (_hadRuntimeError) { Environment.Exit(70); }
    }

    static void Run(string source)
    {
        if (source == null) { throw new NullReferenceException("source code cannot be null"); }
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        var parser = new Parser(tokens);
        var expression = parser.Parse();

        if (_hadError || expression == null) { return; }

        _interpreter.Interpret(expression);
    }

    public static void RuntimeError(RuntimeError error)
    {
        Console.Error.WriteLine($"{error.Message}\n[line {error.Token.Line}]");
        _hadRuntimeError = true;
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




