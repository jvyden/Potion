namespace Potion.Lang.Lexing;

public enum TokenType
{
    // Syntax //
    OpenParentheses, // (
    CloseParentheses, // )
    EndLine, // ;

    // Math //
    Add, // +
    Subtract, // -
    Multiply, // *

    // Keywords //
    Halt,
    Print,
    
    // Literals //
    Identifier, // var name/function name
    StringLiteral, // "string"
    CharLiteral, // 'a'
    IntLiteral, // 42
    
    // Equal //
    Equal, // ==
    NotEqual // !=
}