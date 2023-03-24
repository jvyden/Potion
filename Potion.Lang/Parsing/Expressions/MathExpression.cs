using Potion.Lang.Lexing;
using Potion.Lang.Parsing.Expressions.Literals;

namespace Potion.Lang.Parsing.Expressions;

public class MathExpression : IExpression
{
    public IntLiteralExpression Left;
    public IntLiteralExpression Right;
    public TokenType Operator;
}