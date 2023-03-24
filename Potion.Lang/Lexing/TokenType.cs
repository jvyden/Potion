namespace Potion.Lang.Lexing;

public enum TokenType
{
    // Syntax //
    OpenParentheses, // (
    CloseParentheses, // )
    NextArg, // ,
    EndLine, // ;

    // Math //
    Add, // +
    Subtract, // -
    Multiply, // *

    // Keywords //
    Halt,
    Print,
    Label,
    Jump,
    
    // Literals //
    Identifier, // var name/function name
    StringLiteral, // "string"
    CharLiteral, // 'a'
    IntLiteral, // 42
    
    // Equal //
    Equal, // ==
    NotEqual // !=
}