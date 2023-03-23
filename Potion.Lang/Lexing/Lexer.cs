namespace Potion.Lang.Lexing;

public class Lexer
{
    private readonly List<Token> _tokens = new();
    private string _currentToken = string.Empty;
    
    private void AddCurrentToken()
    {
        AddToken(_currentToken);
    }

    private void AddSyntaxToken(byte token)
    {
        AddCurrentToken();
        AddToken(((char)token).ToString());
    }

    private void AddToken(string token)
    {
        if (token.Length == 0) return;

        // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression (looks like shit)
        if (int.TryParse(token, out int val))
            _tokens.Add(new Token(TokenType.IntLiteral, val.ToString()));
        else
            _tokens.Add(new Token(TokenToType(token), token));
        
        this._currentToken = string.Empty;
    }

    private static TokenType TokenToType(string data) => data switch
    {
        "print" => TokenType.Print,
        "+" => TokenType.Add,
        "-" => TokenType.Subtract,
        "*" => TokenType.Multiply,
        "(" => TokenType.OpenParentheses,
        ")" => TokenType.CloseParentheses,
        ";" => TokenType.EndLine,
        _ => TokenType.Identifier
    };

    public IEnumerable<Token> Analyze(ReadOnlySpan<byte> code)
    {
        foreach (byte b in code)
        {
            if (char.IsWhiteSpace((char)b) && _currentToken.Length > 0) AddCurrentToken();
            else if (b == '(') AddSyntaxToken(b);
            else if (b == ')') AddSyntaxToken(b);
            else if (b == ';') AddSyntaxToken(b);
            else
            {
                _currentToken += (char)b;
            }
        }
        
        // Add the last token if there is one
        AddCurrentToken();

        return _tokens;
    }
}