public class Token
{
    readonly TokenType _type;
    readonly string _lexeme;
    readonly Object _literal;
    readonly int _line;

    public Token(TokenType type, string lexeme, Object literal, int line)
    {
        this._type = type;
        this._lexeme = lexeme;
        this._literal = literal;
        this._line = line;
    }

    public override string ToString()
    {
        return $"Type {_type} Lexeme {_lexeme} Literal {_literal}";
    }
}