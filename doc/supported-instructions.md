# Supported Instructions

$REG = Register number or name, prefixed with `$`.

IMM = A 16-bit signed integer immediate.  
No Prefix = Decimal (base 10)  
`0x` = Hexadecimal (base 16)  
`0b` = Binary (base 2)  

This list can also be printed from the assember using the `instructions` command line argument.

```
add $REG, $REG
add $REG, $REG, IMM
add $REG, IMM
addc $REG, $REG, IMM
addc $REG, IMM
and $REG, $REG
and $REG, $REG, IMM
and $REG, IMM
andc $REG, $REG, IMM
andc $REG, IMM
brc $REG
brc IMM
ceq $REG, $REG
ceq $REG, IMM
cgt $REG, $REG
cgt $REG, IMM
cgtu $REG, $REG
cgtu $REG, IMM
clt $REG, $REG
clt $REG, IMM
clte $REG, $REG
clte $REG, IMM
clteu $REG, $REG
clteu $REG, IMM
cltu $REG, $REG
cltu $REG, IMM
cneq $REG, $REG
cneq $REG, IMM
halt
jmp $REG
jmp IMM
la $REG, $REG
la $REG, IMM
lac $REG, $REG
lac $REG, IMM
lb $REG, $REG
lb $REG, IMM
lb $REG, IMM($REG)
lbu $REG, $REG
lbu $REG, IMM
lbu $REG, IMM($REG)
lh $REG, $REG
lh $REG, IMM
lh $REG, IMM($REG)
li $REG, IMM
lic $REG, IMM
nand $REG, $REG
nand $REG, $REG, IMM
nand $REG, IMM
nandc $REG, $REG, IMM
nandc $REG, IMM
nor $REG, $REG
nor $REG, $REG, IMM
nor $REG, IMM
norc $REG, $REG, IMM
norc $REG, IMM
or $REG, $REG
or $REG, $REG, IMM
or $REG, IMM
orc $REG, $REG, IMM
orc $REG, IMM
rol $REG, $REG
rol $REG, $REG, IMM
rol $REG, IMM
ror $REG, $REG
ror $REG, $REG, IMM
ror $REG, IMM
sb $REG, $REG
sb $REG, IMM
sb $REG, IMM($REG)
sh $REG, $REG
sh $REG, IMM
sh $REG, IMM($REG)
sll $REG, $REG
sll $REG, $REG, IMM
sll $REG, IMM
sra $REG, $REG
sra $REG, $REG, IMM
sra $REG, IMM
srl $REG, $REG
srl $REG, $REG, IMM
srl $REG, IMM
sub $REG, $REG
sub $REG, $REG, IMM
sub $REG, IMM
subc $REG, $REG, IMM
subc $REG, IMM
xnor $REG, $REG
xnor $REG, $REG, IMM
xnor $REG, IMM
xnorc $REG, $REG, IMM
xnorc $REG, IMM
xor $REG, $REG
xor $REG, $REG, IMM
xor $REG, IMM
xorc $REG, $REG, IMM
xorc $REG, IMM
```
