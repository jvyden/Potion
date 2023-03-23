using Potion.Lang.Lexing;

namespace Potion.Lang.Parsing.Expressions;

public class MathExpression : IExpression
{
    public IExpression Left;
    public IExpression Right;
    public TokenType Operator;
}