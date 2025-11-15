


public class LoxFunction : LoxCallable
{
    private readonly Stmt.Function _declaration;
    private readonly Environment _closure;

    public LoxFunction(Stmt.Function declaration, Environment closure)
    {
        _closure = closure;
        _declaration = declaration;
    }

    public int Arity()
    {
        return _declaration.Params.Count;
    }

    public object Call(Interpreter interpreter, Token paren, List<object> args)
    {
        var environment = new Environment(_closure);
        for (int i = 0; i < _declaration.Params.Count; i++)
        {
            environment.Define(_declaration.Params[i].Lexeme, args[i]);
        }
        try
        {
            interpreter.ExecuteBlock(_declaration.Body, environment);
        }
        catch(Return returnEx)
        {
            return returnEx.val;
        }
        return null;
    }
}

