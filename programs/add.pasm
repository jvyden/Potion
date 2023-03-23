# Take two ASCII inputs, convert them to numbers, add them, then print them back to console

# newline
set c 0x0a

rchar a
wchar a
wchar c
sub a 0x30

rchar b
wchar b
wchar c
sub b 0x30

add a %b
add a 0x30
wchar a

wchar c