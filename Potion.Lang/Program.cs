// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Potion.Lang;
using Potion.Lang.Lexing;


ReadOnlySpan<byte> code = @"print(4 + 5);"u8;

Stopwatch stopwatch = new();
stopwatch.Start();

Lexer lexer = new();
List<Token> tokens = lexer.Analyze(code).ToList();

Console.WriteLine("Lexed code in " + stopwatch.ElapsedMilliseconds + "ms");

foreach (Token token in tokens)
{
     Console.WriteLine(token);
}

stopwatch.Restart();

Parser parser = new(tokens);
parser.ParseAll();

Console.WriteLine("Parsed code in " + stopwatch.ElapsedMilliseconds + "ms");
stopwatch.Restart();