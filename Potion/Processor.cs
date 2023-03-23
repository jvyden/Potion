using System.Numerics;
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
            {
                break;
            }

            case InstructionType.Add:
            {
                _registers[instruction.Register] = (byte)(register + instruction.Operand);
                break;
            }
            case InstructionType.Sub:
            {
                _registers[instruction.Register] = (byte)(register - instruction.Operand);
                break;
            }

            case InstructionType.Set:
            {
                _registers[instruction.Register] = instruction.Operand;
                break;
            }

            case InstructionType.Print:
            {
                char c = (char)register;
                if (c == '\n')
                {
                    Console.WriteLine();
                    break;
                }
                
                Console.Write(c);
                break;
            }
            case InstructionType.Read:
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                char c = key.KeyChar;
                if (key.Key == ConsoleKey.Enter) c = '\n'; // would be \r without this

                _registers[instruction.Register] = (byte)c;
                break;
            }
            case InstructionType.Jmp:
            {
                _address = instruction.Operand;
                break;
            }
            case InstructionType.JmpE:
            {
                if (_registers[Register.A] == _registers[Register.B])
                    _address = instruction.Operand;
                break;
            }
            case InstructionType.JmpNe:
            {
                if (_registers[Register.A] != _registers[Register.B])
                    _address = instruction.Operand;
                break;
            }
            case InstructionType.Hlt:
            {
                Console.WriteLine("HALT!");
                Halted = true;
                return;
            }
            default:
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        
        if (_address >= _program.Length) this.Halted = true;
    }

    public void DumpRegistersToConsole()
    {
        foreach ((Register reg, byte val) in _registers)
            Console.WriteLine($"Register {reg}: {val.ToHexString()}");
    }
}