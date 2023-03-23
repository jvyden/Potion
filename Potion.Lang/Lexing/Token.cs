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
}