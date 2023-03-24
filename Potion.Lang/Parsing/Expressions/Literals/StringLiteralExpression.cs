namespace Potion.Lang.Parsing.Expressions.Literals;

public class StringLiteralExpression : ILiteralExpression<string>
{
    public string Value { get; }

    public StringLiteralExpression(string value)
    {
        Value = value;
    }
}