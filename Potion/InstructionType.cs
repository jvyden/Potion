namespace Potion;

public enum InstructionType : byte
{
    Nop = 0x00,
    
    Add = 0xA1,
    Sub = 0xA2,
    /// <summary>
    /// Sets a value in the register
    /// </summary>
    Set = 0xB0,

    /// <summary>
    /// Print a character to the console
    /// </summary>
    Print = 0xD1,
    /// <summary>
    /// Read a character from the console
    /// </summary>
    Read = 0xD2,
    
    Jmp = 0xDA,
    /// <summary>
    /// Jumps if A and B are equal
    /// </summary>
    JmpE = 0xDB,
    /// <summary>
    /// Jumps if A and B are not equal
    /// </summary>
    JmpNe = 0xDC,
    
    /// <summary>
    /// Stops execution
    /// </summary>
    Hlt = 0xDF,
}