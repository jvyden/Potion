using Potion.Extensions;

namespace Potion;

public readonly struct Instruction
{
    public readonly InstructionType Type;
    public readonly Register Register;
    public readonly byte Operand;
    public readonly Register? RegisterReference;

    public Instruction(InstructionType type, byte operand, Register register, Register? regRef)
    {
        Type = type;
        Operand = operand;
        this.Register = register;
        this.RegisterReference = regRef;
    }

    public override string ToString()
    {
        return $"{Type.ToString().ToLower()} {Register.ToString().ToLower()} {Operand.ToHexString()}";
    }
}