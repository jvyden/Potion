// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Potion.Emulator;
using Potion.Lang;
using Potion.Lang.Lexing;
using Potion.Lang.Parsing;
using Potion.Lang.Parsing.Expressions;
using Potion.VirtualMachine;

ReadOnlySpan<byte> code = File.ReadAllBytes(string.Join(' ', args));

Stopwatch totalStopwatch = new();
Stopwatch stopwatch = new();
totalStopwatch.Start();
stopwatch.Start();

Lexer lexer = new();
List<Token> tokens = lexer.Analyze(code).ToList();

Console.WriteLine("Lexed code in " + stopwatch.ElapsedMilliseconds + "ms");

stopwatch.Restart();

Parser parser = new(tokens);
RootExpression expr = parser.ParseAll();

Console.WriteLine("Parsed code in " + stopwatch.ElapsedMilliseconds + "ms");
stopwatch.Restart();

Compiler compiler = new(expr);
Instruction[] program = compiler.Compile();

Console.WriteLine("Compiled code in " + stopwatch.ElapsedMilliseconds + "ms");
Console.WriteLine("Total time to compile: " + totalStopwatch.ElapsedMilliseconds + "ms");

Console.WriteLine("Program Instructions: ");
foreach (Instruction instruction in program)
{
     Console.WriteLine($"  {instruction}");
}

Console.WriteLine();
Console.WriteLine("RUNNING COMPILED PROGRAM!");
Console.WriteLine("-------------------------");

Processor processor = new Processor(program);
while (!processor.Halted)
{
     processor.Cycle();
}