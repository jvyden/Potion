# Fibonacci sequence
# The Fibonacci sequence is a sequence in which each number is the sum of the two preceding ones

set i 0x00 # Setup increment

# First 2 numbers
set a 0x00
set b 0x01

# Skip over reading memory
jmp 0x06

rmem a 0x00
rmem b 0x01

# c = a + b
add a %b
set c %a

wval a

set a %b
set b %c

# Increment loop
add i 0x01

# save to memory
wmem a 0x00
wmem b 0x01

# if(i != 12) continue
set a %i
set b 0x0C

jmpne 0x04