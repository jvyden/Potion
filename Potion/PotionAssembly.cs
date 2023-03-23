namespace Potion;

public static class PotionAssembly
{
    private static bool TryParseOperand(string operandStr, out byte operand, out Register? registerRef)
    {
        // %a (register)
        if (operandStr.StartsWith('%'))
        {
            operand = 0x00;
            if (!Enum.TryParse(operandStr.Substring(1), true, out Register register))
            {
                Console.WriteLine($"REGISTER COULD NOT BE FOUND! What is '{operandStr}'? Not setting operand");
                registerRef = null;
                return false;
            }

            registerRef = register;
            return true;
        }
        
        registerRef = null;
        
        // 'a'
        if (operandStr.StartsWith('\''))
        {
            operand = (byte)operandStr[1];
            return true;
        }
        
        // 0xAA
        if (operandStr.StartsWith("0x"))
        {
            operand = byte.Parse(operandStr.Substring(2), System.Globalization.NumberStyles.HexNumber);
            return true;
        }

        operand = 0x00;
        return false;
    }
    
    public static Instruction[] LoadFromFile(string filename)
    {
        using StreamReader reader = new(File.OpenRead(filename));
        return LoadFromReader(reader);
    }

    private static Instruction[] LoadFromReader(TextReader reader)
    {
        List<Instruction> instructions = new();

        while (reader.ReadLine() is { } line)
        {
            if(line.Length <= 0) continue;
            if(line[0] == '#') continue; // comments
            // Console.WriteLine(line);

            string[] split = line.Split(' ')
                .TakeWhile(s => !s.StartsWith('#'))
                .ToArray();

            if (!Enum.TryParse(split[0], true, out InstructionType type))
            {
                Console.WriteLine($"INSTRUCTION COULD NOT BE FOUND! What is '{split[0]}'? Setting as Nop instead.");
                type = InstructionType.Nop;
            }

            Register reg = Register.A;
            Register? regRef = null;
            byte operand = 0x00;

            if (split.Length > 1)
            {
                if (TryParseOperand(split[1], out operand, out regRef)) reg = Register.A;
                else
                {
                    if (!Enum.TryParse(split[1], true, out reg))
                    {
                        Console.WriteLine($"REGISTER COULD NOT BE FOUND! What is '{split[1]}'? Setting as A instead.");
                        reg = Register.A;
                    }

                    if (split.Length > 2) TryParseOperand(split[2], out operand, out regRef);
                }
            }

            Instruction instruction = new Instruction(type, operand, reg, regRef);
            // Console.WriteLine(instruction);
            instructions.Add(instruction);
        }

        return instructions.ToArray();
    }
}