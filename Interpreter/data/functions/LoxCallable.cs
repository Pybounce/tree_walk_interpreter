

public interface LoxCallable
{
    public int Arity();
    public object Call(Interpreter interpreter, Token paren, List<object> args);
}