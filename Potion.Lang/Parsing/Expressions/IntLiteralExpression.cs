using Potion.Lang.Lexing;

namespace Potion.Lang.Parsing.Expressions;

public class IntLiteralExpression : IExpression
{
    public int Value;

    public IntLiteralExpression(int value)
    {
        Value = value;
    }
}