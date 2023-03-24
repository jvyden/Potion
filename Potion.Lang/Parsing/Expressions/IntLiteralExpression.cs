namespace Potion.Lang.Parsing.Expressions;

public class IntLiteralExpression : IExpression
{
    public readonly int Value;

    public IntLiteralExpression(int value)
    {
        Value = value;
    }
}