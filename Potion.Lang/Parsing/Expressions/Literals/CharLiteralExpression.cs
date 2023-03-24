namespace Potion.Lang.Parsing.Expressions.Literals;

public class CharLiteralExpression : ILiteralExpression<char>
{
    public char Value { get; }

    public CharLiteralExpression(char value)
    {
        Value = value;
    }
}