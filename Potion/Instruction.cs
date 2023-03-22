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
    
    public Instruction(InstructionType type, char operand, Register register = Register.A)
    {
        Type = type;
        Operand = (byte)operand;
    }
}