# Fibonacci sequence
# The Fibonacci sequence is a sequence in which each number is the sum of the two preceding ones

set i 0x00

set a 0x00
set b 0x01

jmp 0x06

rmem a 0x00
rmem b 0x01

add a %b
set c %a

wval a

set a %b
set b %c

add i 0x01

wmem a 0x00
wmem b 0x01

set a %i
set b 0x0C

jmpne 0x04