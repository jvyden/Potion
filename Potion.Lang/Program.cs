// See https://aka.ms/new-console-template for more information

using Potion.Lang.Lexing;

ReadOnlySpan<byte> code = @"print(4 + 5);"u8;

Lexer lexer = new();
IEnumerable<Token> tokens = lexer.Analyze(code);

foreach (Token token in tokens)
{
    Console.WriteLine(token);
}