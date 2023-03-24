using Potion.Lang.Lexing;
using Potion.Lang.Parsing.Expressions;
using Potion.Lang.Parsing.Expressions.Literals;
using Potion.VirtualMachine;

namespace Potion.Lang;

public class Compiler
{
    private readonly List<Instruction> _program = new();
    private readonly RootExpression _root;

    public Compiler(RootExpression root)
    {
        _root = root;
    }

    private void SetRegister(Register reg, int operand)
    {
        _program.Add(new Instruction(InstructionType.Set, (byte)operand, reg, null));
    }

    private void CompileMath(MathExpression expr)
    {
        SetRegister(Register.A, expr.Left.Value);
        SetRegister(Register.B, expr.Right.Value);

        InstructionType type = expr.Operator switch
        {
            TokenType.Add => InstructionType.Add,
            TokenType.Subtract => InstructionType.Sub,
            TokenType.Multiply => InstructionType.Mul,
        };
        
        _program.Add(new Instruction(type, 0x00, Register.A, Register.B));
    }

    private void WriteChar(char c)
    {
        SetRegister(Register.A, (byte)c);
        _program.Add(new Instruction(InstructionType.WChar, 0x00, Register.A, null));
    }

    private void CompilePrint(PrintExpression expr)
    {
        if(expr.Argument is MathExpression math)
            this.CompileMath(math);
        else if (expr.Argument is IntLiteralExpression intExpr)
            SetRegister(Register.A, intExpr.Value);
        else if (expr.Argument is CharLiteralExpression charExpr)
        {
            WriteChar(charExpr.Value);
            return;
        }
        else if (expr.Argument is StringLiteralExpression strExpr)
        {
            foreach (char c in strExpr.Value) WriteChar(c);
            return;
        }
        
        _program.Add(new Instruction(InstructionType.WVal, 0x00, Register.A, null));
    }

    private void CompileHalt(HaltExpression expr)
    {
        _program.Add(new Instruction(InstructionType.Hlt, 0x00, Register.A, null));
    }

    public Instruction[] Compile()
    {
        foreach (IExpression expr in _root.Expressions)
        {
            if(expr is PrintExpression print) CompilePrint(print);
            if(expr is HaltExpression halt) CompileHalt(halt);
        }

        return _program.ToArray();
    }
}