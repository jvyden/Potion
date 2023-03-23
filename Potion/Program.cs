using Potion;

if (args.Length < 1)
{
    Console.WriteLine("You must specify a .pasm file to execute.");
    return;
}

Instruction[] program = PotionAssembly.LoadFromFile(string.Join(' ', args));

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