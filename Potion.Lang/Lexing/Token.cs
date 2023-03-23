namespace Potion.Lang.Lexing;

public readonly struct Token
{
    public readonly TokenType Type;
    public readonly string Data;

    public Token(TokenType type, string data)
    {
        Type = type;
        Data = data;
    }

    public override string ToString()
    {
        return $"{Type.ToString()} {Data}";
    }

    public bool IsMathToken => Type is TokenType.Add or TokenType.Subtract or TokenType.Multiply;

    public bool IsLiteralOrIdentifier => Type is TokenType.CharLiteral or TokenType.IntLiteral
        or TokenType.StringLiteral or TokenType.Identifier;
}