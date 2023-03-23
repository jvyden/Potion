// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Potion.Lang.Lexing;
using Potion.Lang.Parsing;
using Potion.Lang.Parsing.Expressions;


ReadOnlySpan<byte> code = @"print(4 + 8);"u8;

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
RootExpression expr = parser.ParseAll();

Console.WriteLine("Parsed code in " + stopwatch.ElapsedMilliseconds + "ms");
stopwatch.Restart();