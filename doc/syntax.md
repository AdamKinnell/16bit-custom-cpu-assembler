# Syntax

## (E)BNF Instruction Syntax

```
<file> ::= {<line> "\n"}
<line> ::= [<label>] [<instruction>] [<comment>]

<label>      ::= <label name> ":"
<label name> ::= ?([a-zA-Z][a-zA-Z0-9_]+)?
<comment>    ::= "#" {character}

<instruction> ::= <mnemonic> <*_type>
<mnemonic>    ::= ?([a-ZA-Z]+)?
<free_type>   ::= 
<1r_type>     ::= <register>`
<1i_type>     ::= <immediate>
<2r_type>     ::= <register> [","] <register>
<2r1i_type>   ::= <register> [","] <register> [","] <immediate>
<1rboff_type> ::= <immediate> "(" <register> ")"
<2rboff_type> ::= <register> [","] <immediate> "(" <register> ")"

<register>        ::= "$" (<register number> | <register name>)
<register number> ::= ?(0 to 15)?
<register name>   ::= "zero" | "at" | ("t" ?(0 to 5)?) | "s0"
                    | ("ar" ?(0 to 3)?) | "sp" | "pc" | "status"

<immediate>   ::= <integer> | <label name>
<integer>     ::= <decimal> | <hexadecimal> | <binary>
<decimal>     ::= ?([0-9]+)?
<hexadecimal> ::= "0x" ?([0-9a-fA-F]+)?
<binary>      ::= "0b" ?([0,1]+)?
```

## Real Instructions
**AND/NAND/OR/NOR/XOR/XNOR/ADD/SUB**  
`OP  $r1, $r2` -> `$r1 = $r1 OP $r2`  
`OP  $r1, $r2, imm` -> `$r1 = $r2 OP imm`  
`OPC $r1, $r2, imm` -> `if (compare): $r1 = $r2 OP imm`

**SLL/SRL/SRA/ROL/ROR**  
`OP $r1, $r2` -> `$r1 = $r1 OP $r2`  
`OP $r1, $r2, imm` -> `$r1 = $r2 OP imm`

**CEQ/CNEQ/CLT/CLTE/CLTEU/CGT/CGTU**  
`OP $r1, $r2` -> `compare = $r1 OP $r2`  
`OP $r1, $r2, imm` -> `compare = $r2 OP imm`

**LB/LBU/LH**  
`OP $r1, $r2` -> `$r1 = *$r2`  
`OP $r1, imm($r2)` -> `$r1 = *($r2 + imm bytes)`  
`OP $r1, lbl` -> `$r1 = *lbl`

**SB/SH**  
`OP $r1, $r2` -> `*$r2 = $r1`  
`OP $r1, imm($r2)` -> `*($r2 + imm bytes) = $r1`  
`OP $r1, lbl` -> `*lbl = $r1`

**Other**  
`HALT` -> `Halt CPU`

## Pseudo Instructions

### Data Transfer
| Instruction   | Operation               | Native              | Implemented? |
|---------------|-------------------------|---------------------|--------------|
| LI $r1, imm   | $r1 = imm               | OR $r1, $zero, imm  | Yes          |
| LIC $r1, imm  | if (compare): $r1 = imm | ORC $r1, $zero, imm | Yes          |
| LA $r1, lbl   | $r1 = lbl               | OR $r1, $zero, lbl  | **No**          |
| LAC $r1, lbl  | if (compare): $r1 = lbl | ORC $r1, $zero, lbl | **No**          |
| MOV $r1, $r2  | $r1 = $r2               | OR $r1, $r2, 0      | **No**           |
| MOVC $r1, $r2 | if (compare): $r1 = $r2 | ORC $r1, $r2, 0     | **No**           |

### Control Transfer
| Instruction | Operation               | Native        | Implemented? |
|-------------|-------------------------|---------------|--------------|
| JMP imm     | $pc = imm               | LI $pc, imm   | Yes          |
| JMP lbl     | $pc = lbl               | LA $pc, lbl   | **No**          |
| JMP $r1     | $pc = $r1               | MOV $pc, $r1  | Yes          |
| BRC imm      | if (compare): $pc = imm | LIC $pc, imm  | Yes          |
| BRC lbl      | if (compare): $pc = lbl | LAC $pc, lbl  | **No**          |
| BRC $r1      | if (compare): $pc = $r1 | MOVC $pc, $r1 | Yes          |
| BEQ         | if ($r1 == $r2) JMP     |               | **No**           |
| BNE         | if ($r1 != $r2) JMP     |               | **No**           |
| BLT         | if ($r1 < $r2) JMP      |               | **No**           |
| BLTE        | if ($r1 <= $r2) JMP     |               | **No**           |
| BGT         | if ($r1 > $r2) JMP      |               | **No**           |
| BGTE        | if ($r1 >= $r2) JMP     |               | **No**           |
| BLTU        | if ($r1 < $r2) JMP      |               | **No**           |
| BLTEU       | if ($r1 <= $r2) JMP     |               | **No**           |
| BGTU        | if ($r1 > $r2) JMP      |               | **No**           |
| BGTEU       | if ($r1 >= $r2) JMP     |               | **No**           |
