# Take a line of text, write it to memory, dump memory, and then print it back

rchar a
wchar a # loopback
wmem a %c # write A register to memory at register C
add c 0x01 # C is an increment, used for memory offset
set b 0x0a # newline
jmpe 0x07 # jump if character was a newline
jmp 0x00

# jumps here
dmp # dump memory to disk
set c 0x00 # reset increment
rmem a %c
wchar a
add c 0x01
jmpne 0x09