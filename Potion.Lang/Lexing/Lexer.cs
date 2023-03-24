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
        "halt" => TokenType.Halt,
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
        bool readingString = false;
        bool readingChar = false;
        bool readingEscape = false;
        
        foreach (byte b in code)
        {
            if (readingString || readingChar)
            {
                char end = readingChar ? '\'' : '"';

                if ((char)b == '\\')
                {
                    readingEscape = true;
                    _currentToken += (char)b;
                    continue;
                }

                if (readingEscape)
                {
                    char[] tokenArr = _currentToken.ToCharArray();
                    tokenArr[^1] = (char)b switch
                    {
                        '0' => '\0',
                        'n' => '\n',
                        _ => '?'
                    };

                    _currentToken = new string(tokenArr);
                    readingEscape = false;
                    continue;
                }
                
                if ((char)b == end)
                {
                    TokenType type = readingChar ? TokenType.CharLiteral : TokenType.StringLiteral;
                    readingChar = false;
                    readingString = false;
                    
                    _tokens.Add(new Token(type, _currentToken));
                    _currentToken = string.Empty;
                    
                    continue;
                }
                
                _currentToken += (char)b;
                continue;
            }
            
            if (char.IsWhiteSpace((char)b))
            {
                if(_currentToken.Length > 0) AddCurrentToken();
            }
            else if (b == '(') AddSyntaxToken(b);
            else if (b == ')') AddSyntaxToken(b);
            else if (b == ';') AddSyntaxToken(b);
            else if (b == '"') readingString = true;
            else if (b == '\'') readingChar = true;
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