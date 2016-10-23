using System;
using Assembler.Constants;
using Assembler.Operands;
using Assembler.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Contains methods for creating instruction definitions.
    /// </summary>
    class InstructionFactory {

        // Static Fields //////////////////////////////////////////////////////

        private static readonly Type REGISTER_TYPE = typeof(RegisterOperand);
        private static readonly Type IMMEDIATE_TYPE = typeof(ImmediateOperand);
        private static readonly Type BASEOFFSET_TYPE = typeof(BaseOffsetOperand);

        // Static Functions ///////////////////////////////////////////////////

        /// <summary>
        ///     Create a standard RR format instruction.
        /// 
        ///     With Mapping:
        ///     -   R1  = First Operand
        ///     -   R2  = Second Operand
        ///     -   IMM = 0
        /// </summary>
        [NotNull]
        public static NativeInstruction CreateRRStandardInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstruction(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, REGISTER_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .R2(ops => (int) ((RegisterOperand) ops[1]).RegisterNumber)
                    .Immediate(0)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create a standard RI format instruction.
        /// 
        ///     With Mapping:
        ///     -   R1  = First Operand
        ///     -   R2  = Second Operand
        ///     -   IMM = Third Operand
        /// </summary>
        [NotNull]
        public static NativeInstruction CreateRIStandardInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstruction(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, REGISTER_TYPE, IMMEDIATE_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .R2(ops => (int) ((RegisterOperand) ops[1]).RegisterNumber)
                    .Immediate(ops => ((ImmediateOperand) ops[2]).Value)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create an RI format instruction
        ///     with the destination implicity defined as the first (register) operand.
        /// 
        ///     With Mapping:
        ///     -   R1  = First Operand
        ///     -   R2  = First Operand
        ///     -   IMM = Second Operand
        /// </summary>
        [NotNull]
        public static NativeInstruction CreateRIImplicitDestinationInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstruction(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, IMMEDIATE_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .R2(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .Immediate(ops => ((ImmediateOperand) ops[1]).Value)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create an RI format instruction
        ///     where the destination register is unused.
        /// 
        ///     With Mapping:
        ///     -   R1  = Zero
        ///     -   R2  = First Operand
        ///     -   IMM = Second Operand
        /// </summary>
        [NotNull]
        public static NativeInstruction CreateRIDestinationUnusedInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstruction(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, IMMEDIATE_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) Registers.RegisterNumber.ZERO)
                    .R2(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .Immediate(ops => ((ImmediateOperand) ops[1]).Value)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

    }
}