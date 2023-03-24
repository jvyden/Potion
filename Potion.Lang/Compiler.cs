using Potion.Lang.Lexing;
using Potion.Lang.Parsing.Expressions;
using Potion.Lang.Parsing.Expressions.Literals;
using Potion.VirtualMachine;

namespace Potion.Lang;

public class Compiler
{
    private readonly List<Instruction> _program = new();
    private readonly RootExpression _root;

    private readonly Dictionary<string, int> _labels = new();

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

    private void CompileLabel(LabelExpression expr)
    {
        if (_labels.ContainsKey(expr.LabelName))
            throw new Exception($"Label {expr.LabelName} already exists");
        
        _labels.Add(expr.LabelName, _program.Count);
    }

    private void CompileJump(JumpExpression expr)
    {
        if (!_labels.ContainsKey(expr.LabelName))
            throw new Exception($"Label {expr.LabelName} does not exist!");

        _program.Add(new Instruction(InstructionType.Jmp, (byte)_labels[expr.LabelName], Register.A, null));
    }

    public Instruction[] Compile()
    {
        foreach (IExpression expr in _root.Expressions)
        {
            if(expr is PrintExpression print) CompilePrint(print);
            else if(expr is HaltExpression halt) CompileHalt(halt);
            else if(expr is LabelExpression label) CompileLabel(label);
            else if(expr is JumpExpression jump) CompileJump(jump);
        }

        return _program.ToArray();
    }
}