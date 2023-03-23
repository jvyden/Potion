using Potion.Extensions;

namespace Potion;

public class Processor
{
    private int _address;
    private readonly Instruction[] _program;

    private readonly Dictionary<Register, byte> _registers = new();

    public Processor(Instruction[] program)
    {
        // Initialize registers
        foreach (Register reg in Enum.GetValues<Register>()) 
            _registers[reg] = 0x00;

        this._program = program;
        this._address = 0;
    }
    
    public bool Halted { get; private set; }

    public void Cycle()
    {
        Instruction instruction = _program[_address];
        _address++;

        byte register = _registers[instruction.Register];

        switch (instruction.Type)
        {
            case InstructionType.Nop:
                break;
        
            case InstructionType.Add:
                _registers[instruction.Register] = (byte)(register + instruction.Operand);
                break;
            case InstructionType.Sub:
                _registers[instruction.Register] = (byte)(register - instruction.Operand);
                break;

            case InstructionType.Set:
                _registers[instruction.Register] = instruction.Operand;
                break;

            case InstructionType.Print:
                Console.Write((char)register);
                break;
            case InstructionType.Read:
                _registers[instruction.Register] = (byte)Console.ReadKey().KeyChar;
                break;
            case InstructionType.Jmp:
                _address = instruction.Operand;
                break;
            case InstructionType.JmpE:
                if (_registers[Register.A] == _registers[Register.B])
                    _address = instruction.Operand;
                break;
            case InstructionType.JmpNe:
                if (_registers[Register.A] != _registers[Register.B])
                    _address = instruction.Operand;
                break;
            case InstructionType.Hlt:
                Console.WriteLine("HALT!");
                Halted = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        if (_address >= _program.Length) this.Halted = true;
    }

    public void DumpRegistersToConsole()
    {
        foreach ((Register reg, byte val) in _registers)
            Console.WriteLine($"Register {reg}: {val.ToHexString()}");
    }
}