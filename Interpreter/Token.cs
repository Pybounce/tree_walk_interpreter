public class Token
{
    public readonly TokenType TokenType;
    public readonly string Lexeme;
    public readonly Object Literal;
    public readonly int Line;

    public Token(TokenType type, string lexeme, Object literal, int line)
    {
        this.TokenType = type;
        this.Lexeme = lexeme;
        this.Literal = literal;
        this.Line = line;
    }

    public override string ToString()
    {
        return $"Type {TokenType} Lexeme {Lexeme} Literal {Literal}";
    }
}