using Potion.Lang.Lexing;

namespace Potion.Lang.Parsing.Expressions;

public class MathExpression : IExpression
{
    public IntLiteralExpression Left;
    public IntLiteralExpression Right;
    public TokenType Operator;
}