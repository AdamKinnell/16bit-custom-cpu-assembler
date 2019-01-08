# 16-bit Custom CPU Assembler
Simple assembler written in C# for use with my [16bit-custom-cpu](https://github.com/AdamKinnell/16bit-custom-cpu).

## Usage

Run with one of the following command line arguments:

>`assemble <source_file_in> <binary_file_out>`  
>Assemble the source code in <source_file_in> and write the machine code to <binary_file_out>
>
>`assemble <source_file_in>`  
Assemble the source code in <source_file_in> and write the machine code to './out'
>
> `assemble`  
>Assemble the source code in './in' and write the machine code to './out'

Each instruction must be on its own line.

## Unsupported Features
- Labels & named jumps
- Pseudoinstructions with more than 1 native instruction
- Character immediates
- Assembler directives & macros
- Static data and arrays; Anything in the data segment
