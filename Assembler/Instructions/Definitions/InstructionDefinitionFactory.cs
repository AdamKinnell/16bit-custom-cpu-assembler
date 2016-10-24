using System;
using Assembler.Constants;
using Assembler.Operands;
using Assembler.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Instructions.Definitions {

    /// <summary>
    ///     Contains methods for creating instruction definitions.
    /// </summary>
    class InstructionDefinitionFactory {

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
        ///     -   IMM = Zero
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateRRInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstructionDefinition(
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
        public static NativeInstructionDefinition CreateRIStandardInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstructionDefinition(
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
        public static NativeInstructionDefinition CreateRIImplicitDestinationInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstructionDefinition(
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
        ///     Create an instruction
        ///     where the destination register is predefined as {destination}.
        /// 
        ///     With Mapping:
        ///     -   R1  = {destination}
        ///     -   R2  = First Operand
        ///     -   IMM = Second Operand
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateRIDestinationRegisterStaticInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function,
                                                                                               Registers.RegisterNumber destination)
            => new NativeInstructionDefinition(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, IMMEDIATE_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) destination)
                    .R2(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .Immediate(ops => ((ImmediateOperand) ops[1]).Value)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create an instruction
        ///     where the destination register is unused and set to $zero.
        /// 
        ///     With Mapping:
        ///     -   R1  = Zero
        ///     -   R2  = First Operand
        ///     -   IMM = Second Operand
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateRIDestinationRegisterUnusedInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => CreateRIDestinationRegisterStaticInstruction(mnemonic, opcode, function, Registers.RegisterNumber.ZERO);

        /// <summary>
        ///     Create an instruction
        ///     where the destination register is predefined as {source}.
        /// 
        ///     With Mapping:
        ///     -   R1  = First Operand
        ///     -   R2  = {source}
        ///     -   IMM = Second Operand
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateRISourceRegisterStaticInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function,
                                                                                          Registers.RegisterNumber source)
            => new NativeInstructionDefinition(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, IMMEDIATE_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .R2(ops => (int) source)
                    .Immediate(ops => ((ImmediateOperand) ops[1]).Value)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create an instruction
        ///     where the source register is unused and set to $zero.
        /// 
        ///     With Mapping:
        ///     -   R1  = First Operand
        ///     -   R2  = Zero
        ///     -   IMM = Second Operand
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateRISourceRegisterUnusedInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => CreateRISourceRegisterStaticInstruction(mnemonic, opcode, function, Registers.RegisterNumber.ZERO);

        /// <summary>
        ///     Create an RI format instruction
        ///     with Source & Immediate specified in base-offset notation.
        /// 
        ///     With Mapping:
        ///     -   R1  = First Operand
        ///     -   R2  = Base
        ///     -   IMM = Offset
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateBaseOffsetInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstructionDefinition(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, BASEOFFSET_TYPE),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                    .R2(ops => (int) ((BaseOffsetOperand) ops[1]).Base.RegisterNumber)
                    .Immediate(ops => ((BaseOffsetOperand) ops[1]).Offset.Value)
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create an instruction
        ///     where only the opcode field is used.
        /// 
        ///     With Mapping:
        ///     -   Func = Zero
        ///     -   R1   = Zero
        ///     -   R2   = Zero
        ///     -   IMM  = Zero
        /// </summary>
        [NotNull]
        public static NativeInstructionDefinition CreateOpcodeOnlyInstruction([NotNull] string mnemonic, Int32 opcode)
            => new NativeInstructionDefinition(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(),
                mapping: new InstructionFieldMappingBuilder()
                    .R1(ops => (int) Registers.RegisterNumber.ZERO)
                    .R2(ops => (int) Registers.RegisterNumber.ZERO)
                    .Immediate(0)
                    .Function(0)
                    .Opcode(opcode)
                    .Build()
            );
    }
}