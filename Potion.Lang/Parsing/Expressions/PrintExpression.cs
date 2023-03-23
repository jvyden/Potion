using Potion.Lang.Lexing;

namespace Potion.Lang.Parsing.Expressions;

public class PrintExpression : IExpression
{
    public IExpression Argument;
}