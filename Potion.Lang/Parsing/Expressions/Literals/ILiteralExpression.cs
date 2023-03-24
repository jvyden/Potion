namespace Potion.Lang.Parsing.Expressions.Literals;

public interface ILiteralExpression<out TValue> : IExpression
{
    public TValue Value { get; }
}