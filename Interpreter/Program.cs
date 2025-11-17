

class Lox
{

    private static bool _hadError = false;
    private static bool _hadRuntimeError = false;
    private static Interpreter _interpreter = new Interpreter();

    static void Main(string[] args)
    {
        if (TryListExamples(args)) { return; }
        if (TryShowHelp(args)) { return; }
        if (TryRunExample(args)) { return; }

        if (args.Length == 1) { RunFile(args[0]); }
        else if (args.Length == 0) { RunPrompt(); }
        else
        {
            throw new ArgumentException("Use 'dotnet run --help' for instructions.");
        }
    }

    static bool TryShowHelp(string[] args)
    {
        if (args == null || args.Length != 1 || args[0] != "--help") { return false; }

        Console.WriteLine("");
        Console.WriteLine("------------ Instructions ------------");
        Console.WriteLine("Use 'dotnet run' to run lox in REPL mode.");
        Console.WriteLine("Use 'dotnet run FILE_PATH' to run a lox script found at FILE_PATH.");
        Console.WriteLine("Use 'dotnet run --examples EXAMPLE_NAME' to run a built in example.");
        Console.WriteLine("Use 'dotnet run --examples' to list all available example names.");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("");

        return true;
    }

    static bool TryListExamples(string[] args)
    {
        if (args == null || args.Length != 1 || args[0] != "--examples") { return false; }
        var examplesDir = GetExamplesDirectory();
        Console.WriteLine("");
        Console.WriteLine("------------ Examples ------------");
        foreach (var example_path in System.IO.Directory.EnumerateFiles(examplesDir))
        {
            Console.WriteLine(Path.GetFileNameWithoutExtension(example_path));
        }
        Console.WriteLine("----------------------------------");
        Console.WriteLine("");
        return true;
    }

    static bool TryRunExample(string[] args)
    {
        if (args == null || args.Length != 2 || args[0] != "--examples") { return false; }

        try
        {
            var example_name = args[1];
            var path = Path.Combine(GetExamplesDirectory(), $"{example_name.ToLower()}.lox");
            RunFile(path);
        }
        catch(Exception e)
        {
            Console.WriteLine($"Failed to run example. {e.Message}");
            return true;
        }
        return true;

    }

    static string GetExamplesDirectory()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var projectDir = Directory.GetParent(baseDir)?.Parent?.Parent?.Parent?.FullName;
        var path = Path.Combine(projectDir!, "examples");
        return path;
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
        if (_hadError) { System.Environment.Exit(65); }
        if (_hadRuntimeError) { System.Environment.Exit(70); }
    }

    static void Run(string source)
    {
        if (source == null) { throw new NullReferenceException("source code cannot be null"); }
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        var parser = new Parser(tokens);
        var statements = parser.Parse();

        if (_hadError || statements == null) { return; }

        _interpreter.Interpret(statements);
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




