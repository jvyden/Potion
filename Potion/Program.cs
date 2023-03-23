using Potion;

// Instruction[] program = {
//     // new(InstructionType.Set, 's'),
//     // new(InstructionType.Print),
//     // new(InstructionType.Set, 'e'),
//     // new(InstructionType.Print),
//     // new(InstructionType.Set, 'x'),
//     // new(InstructionType.Print),
//     new(InstructionType.Read),
//     new(InstructionType.Print),
//     new(InstructionType.Jump),
//     // new(InstructionType.Set, '\n'),
//     // new(InstructionType.Print),
// };

if (args.Length < 1)
{
    Console.WriteLine("You must specify a .pasm file to execute.");
    return;
}

Instruction[] program = PotionAssembly.LoadFromFile(string.Join(' ', args));
// return;

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