using Microsoft.VisualBasic;

public class Scanner
{

    private readonly string _source;
    private readonly List<Token> _tokens = new List<Token>();

    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    private Dictionary<string, TokenType> _keyword_store;

    public Scanner(string source)
    {
        _source = source;
        _keyword_store = new Dictionary<string, TokenType>()
        {
            { "and", TokenType.AND },
            { "class", TokenType.CLASS },
            { "else", TokenType.ELSE },
            { "false", TokenType.FALSE },
            { "for", TokenType.FOR },
            { "fun", TokenType.FUN },
            { "if", TokenType.IF },
            { "nil", TokenType.NIL },
            { "or", TokenType.OR },
            { "return", TokenType.RETURN },
            { "super", TokenType.SUPER },
            { "this", TokenType.THIS },
            { "true", TokenType.TRUE },
            { "var", TokenType.VAR },
            { "while", TokenType.WHILE },
        };
    }

    public List<Token> ScanTokens()
    {
        while (!isAtEnd())
        {
            _start = _current;
            scanToken();
        }
        _tokens.Add(new Token(TokenType.EOF, "", null, _line));
        return _tokens;
    }

    private void scanToken()
    {
        var c = advance();
        switch (c)
        {
            case '(': addToken(TokenType.LEFT_PAREN); break;
            case ')': addToken(TokenType.RIGHT_PAREN); break;
            case '{': addToken(TokenType.LEFT_BRACE); break;
            case '}': addToken(TokenType.RIGHT_BRACE); break;
            case ',': addToken(TokenType.COMMA); break;
            case '.': addToken(TokenType.DOT); break;
            case '-': addToken(TokenType.MINUS); break;
            case '+': addToken(TokenType.PLUS); break;
            case ';': addToken(TokenType.SEMICOLON); break;
            case '*': addToken(TokenType.STAR); break;
            case '!':
                addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                break;
            case '=':
                addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                break;
            case '<':
                addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                break;
            case '>':
                addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                break;
            case '/':
                if (match('/'))
                {
                    while (peek() != '\n' && !isAtEnd()) { advance(); }
                }
                else { addToken(TokenType.SLASH); }
                break;
            case ' ': break;
            case '\r': break;
            case '\t': break;
            case '\n': _line++; break;
            case '"': stringToken(); break;
            default:
                if (isDigit(c)) { numberToken(); }
                else if (isAlpha(c)) { identifier(); }
                else { Lox.Error(_line, "Unexpected character"); }
                break;
        }
    }

    private bool isAlpha(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
    }

    private bool isAlphaNumeric(char c)
    {
        return isAlpha(c) || isDigit(c);
    }

    private void identifier()
    {
        while (isAlphaNumeric(peek())) { advance(); }
        var text = _source.Substring(_start, _current - _start);

        if (_keyword_store.TryGetValue(text, out TokenType token_type))
        {
            addToken(token_type);
        }
        else
        {
            addToken(TokenType.IDENTIFIER);
        }
    }

    private bool isDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    private void numberToken()
    {
        while (isDigit(peek())) { advance(); }
        if (peek() == '.' && isDigit(peekNext()))
        {
            advance();
            while (isDigit(peek())) { advance(); }
        }

        addToken(TokenType.NUMBER, Double.Parse(_source.Substring(_start, _current - _start)));
    }
    private char peekNext() {
        if (_current + 1 >= _source.Length) { return '\0'; }
        return _source[_current + 1];
    }
    private void stringToken()
    {
        while (peek() != '"' && !isAtEnd())
        {
            if (peek() == '\n') { _line += 1; }
            advance();
        }

        if (isAtEnd())
        {
            Lox.Error(_line, "Unterminated string");
            return;
        }

        // Move past the closing "
        advance();

        var val = _source.Substring(_start + 1, _current - (_start + 1) - 1);
        addToken(TokenType.STRING, val);
    }
    
    private bool isAtEnd()
    {
        return _current >= _source.Length;
    }

    private char advance()
    {
        return _source[_current++];
    }

    private bool match(char expected)
    {
        if (isAtEnd()) { return false; }
        if (_source[_current] != expected) { return false; }
        _current += 1;
        return true;
    }

    private char peek()
    {
        if (isAtEnd()) { return '\0'; }
        return _source[_current];
    }

    private void addToken(TokenType type)
    {
        addToken(type, null);
    }
    
    private void addToken(TokenType type, Object literal)
    {
        var text = _source.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, text, literal, _line));
    }
}

