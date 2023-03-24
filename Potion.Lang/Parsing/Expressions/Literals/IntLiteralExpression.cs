namespace Potion.Lang.Parsing.Expressions.Literals;

public class IntLiteralExpression : ILiteralExpression<int>
{
    public int Value { get; }

    public IntLiteralExpression(int value)
    {
        this.Value = value;
    }

}