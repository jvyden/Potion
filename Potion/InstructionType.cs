namespace Potion;

public enum InstructionType : byte
{
    Nop = 0x00,
    
    Add = 0xA1,
    Sub = 0xA2,
    Set = 0xB0,

    Print = 0xD1,
    Read = 0xD2,
    Jump = 0xDE,
    Halt = 0xDF,
}