using System.Diagnostics;
using Potion.Extensions;

namespace Potion;

public class Processor
{
    private int _address;
    private readonly Instruction[] _program;

    private readonly Dictionary<Register, byte> _registers = new();

    private const int MemorySize = 1024 * 2;
    private readonly byte[] _memory;

    public Processor(Instruction[] program)
    {
        // Initialize registers
        foreach (Register reg in Enum.GetValues<Register>()) 
            _registers[reg] = 0x00;

        this._program = program;
        this._address = 0;

        this._memory = new byte[MemorySize];
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

            case InstructionType.WChar:
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
            case InstructionType.RChar:
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                char c = key.KeyChar;
                if (key.Key == ConsoleKey.Enter) c = '\n'; // would be \r without this

                _registers[instruction.Register] = (byte)c;
                break;
            }

            case InstructionType.RMem:
            {
                _registers[instruction.Register] = _memory[_registers[Register.C]];
                break;
            }
            
            case InstructionType.WMem:
            {
                _memory[_registers[Register.C]] = _registers[instruction.Register];
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

            case InstructionType.Dmp:
            {
                File.WriteAllBytes("memory.bin", _memory);
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