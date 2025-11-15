

class ASTGenerator
{
    static void Main(string[] args)
    {
        if (args.Length != 0)
        {
            throw new ArgumentException("Must have exactly zero arguments");
        }

        var types = new List<string>
        {
            "Binary : Expr left, Token op, Expr right",
            "Grouping : Expr expression",
            "Literal : Object value",
            "Unary : Token op, Expr right",
            "Variable : Token name",
            "Assign : Token name, Expr value",
            "Logical : Expr Left, Token Op, Expr Right",
            "Call : Expr Callee, Token Paren, List<Expr> Arguments"
        };
        DefineAst(Directory.GetCurrentDirectory(), "Expr", types);

        DefineAst(Directory.GetCurrentDirectory(), "Stmt", new List<string>
        {
            "Expression : Expr expression",
            "Print : Expr expression",
            "Var : Token name, Expr initialiser",
            "Block : List<Stmt> statements",
            "If : Expr Condition, Stmt ThenBranch, Stmt? ElseBranch",
            "While : Expr Condition, Stmt Body",
            "Function : Token Name, List<Token> Params, List<Stmt> Body",
            "Return : Token Keyword, Expr expression"
        });

    }


    private static void DefineAst(string outputDir, string baseName, List<string> types)
    {
        var path = Path.Combine(outputDir, $"{baseName}.cs");
        Console.WriteLine(path);
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine($"public abstract class {baseName}");
            writer.WriteLine("{");

            writer.WriteLine($"public abstract R Accept<R>(Visitor<R> visitor);");


            DefineVisitor(writer, baseName, types);

            foreach (var type in types)
            {
                var className = type.Split(":")[0].Trim();
                var fields = type.Split(":")[1].Trim();
                DefineType(writer, baseName, className, fields);
            }


            writer.WriteLine("}");
            writer.Close();
        }
    }

    private static void DefineVisitor(StreamWriter writer, string baseName, List<string> types)
    {
        writer.WriteLine($"public interface Visitor<R>");
        writer.WriteLine("{");
        foreach (var type in types)
        {
            var typeName = type.Split(":")[0].Trim();
            writer.WriteLine($"public R Visit{typeName}{baseName}({typeName} {baseName.ToLower()});");
        }
        writer.WriteLine("}");
    }

    private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
    {
        writer.WriteLine($"public class {className}: {baseName}");
        writer.WriteLine("{");

        var fields = fieldList.Split(", ");
        foreach (var field in fields)
        {
            var type = field.Split(" ")[0];
            var name = field.Split(" ")[1];
            writer.WriteLine($"public readonly {type} {name};");
        }

        writer.WriteLine($"public override R Accept<R>(Visitor<R> visitor)");
        writer.WriteLine("{");
        writer.WriteLine($"return visitor.Visit{className}{baseName}(this);");
        writer.WriteLine("}");

        writer.WriteLine($"public {className}({fieldList}): base()");
        writer.WriteLine("{");

        foreach (var field in fields)
        {
            var name = field.Split(" ")[1];
            writer.WriteLine($"this.{name} = {name};");
        }

        writer.WriteLine("}");

        writer.WriteLine("}");
    }

}