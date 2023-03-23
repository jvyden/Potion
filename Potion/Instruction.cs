using Potion.Extensions;

namespace Potion;

public readonly struct Instruction
{
    public readonly InstructionType Type;
    public readonly Register Register;
    public readonly byte Operand;

    public Instruction(InstructionType type, byte operand = 0x00, Register register = Register.A)
    {
        Type = type;
        Operand = operand;
    }

    public override string ToString()
    {
        return $"{Type.ToString().ToLower()} {Register.ToString().ToLower()} {Operand.ToHexString()}";
    }
}