using Potion;

Instruction[] program = {
    new(InstructionType.Set, 's'),
    new(InstructionType.Print),
    new(InstructionType.Set, 'e'),
    new(InstructionType.Print),
    new(InstructionType.Set, 'x'),
    new(InstructionType.Print),
    new(InstructionType.Set, 0x00),
    new(InstructionType.Jump),
    // new(InstructionType.Set, '\n'),
    // new(InstructionType.Print),
};

Processor processor = new(program);
try
{
    while (!processor.Halted)
    {
        processor.Cycle();
    }
}
catch(Exception e)
{
    Console.WriteLine("Program halted due to exception: " + e);
    processor.DumpRegistersToConsole();
    
    Environment.Exit(1);
    return;
}

Console.WriteLine("Program complete");
processor.DumpRegistersToConsole();