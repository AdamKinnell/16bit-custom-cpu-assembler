using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using Assembler.Operands;
using Assembler.Operands.Types;
using Assembler.Registries;
using JetBrains.Annotations;

namespace Assembler.Constants {

    /// <summary>
    ///     Contains definitions of all native instructions in the architecture.
    /// </summary>
    public class Instructions {

        // Static Fields //////////////////////////////////////////////////////

        private static readonly InstructionRegistry INSTRUCTION_REGISTRY = new InstructionRegistry();

        private static readonly Type REGISTER_TYPE = typeof(RegisterOperand);
        private static readonly Type IMMEDIATE_TYPE = typeof(ImmediateOperand);
        private static readonly Type BASEOFFSET_TYPE = typeof(BaseOffsetOperand);

        // Static Constructors ////////////////////////////////////////////////

        static Instructions() {
            RegisterALUInstructions(INSTRUCTION_REGISTRY);
            RegisterShifterInstructions(INSTRUCTION_REGISTRY);
            RegisterComparisonInstructions(INSTRUCTION_REGISTRY);
            RegisterMemoryInstructions(INSTRUCTION_REGISTRY);
            RegisterDataTransferInstructions(INSTRUCTION_REGISTRY);
            RegisterControlTransferInstructions(INSTRUCTION_REGISTRY);
            RegisterMiscInstructions(INSTRUCTION_REGISTRY);
        }

        // Helper Functions ///////////////////////////////////////////////////

        /// <summary>
        ///     Get a mapping builder for an RR type instruction.
        /// 
        ///     R1 = First Operand
        ///     R2 = Second Operand
        ///     i.e. MNEM $R1, $R2
        /// </summary>
        [NotNull]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static InstructionFieldMappingBuilder GetRRBuilder()
            => new InstructionFieldMappingBuilder()
                .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                .R2(ops => (int) ((RegisterOperand) ops[1]).RegisterNumber)
                .Immediate(0);

        /// <summary>
        ///     Get a mapping builder for an RI type instruction.
        /// 
        ///     R1 = First Operand
        ///     R2 = Second Operand
        ///     IMM = Third Operand
        ///     i.e. MNEM $R1, $R2, IMM
        /// </summary>
        [NotNull]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static InstructionFieldMappingBuilder GetRIBuilder()
            => new InstructionFieldMappingBuilder()
                .R1(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                .R2(ops => (int) ((RegisterOperand) ops[1]).RegisterNumber)
                .Immediate(ops => ((ImmediateOperand) ops[2]).Value);

        /// <summary>
        ///     Get a mapping builder for an RI type instruction
        ///     where the destination register is unused.
        /// 
        ///     R1 = Zero
        ///     R2 = First Operand
        ///     IMM = Second Operand
        ///     i.e. MNEM $R2, IMM
        /// </summary>
        [NotNull]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static InstructionFieldMappingBuilder GetRIDestinationUnusedBuilder()
            => new InstructionFieldMappingBuilder()
                .R1(ops => (int) Registers.RegisterNumber.ZERO)
                .R2(ops => (int) ((RegisterOperand) ops[0]).RegisterNumber)
                .Immediate(ops => ((ImmediateOperand) ops[1]).Value);

        /// <summary>
        ///     Create a native RR format instruction.
        /// </summary>
        [NotNull]
        private static NativeInstruction CreateRRInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstruction(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, REGISTER_TYPE),
                mapping: GetRRBuilder()
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        /// <summary>
        ///     Create a native RI format instruction.
        /// </summary>
        [NotNull]
        private static NativeInstruction CreateRIInstruction([NotNull] string mnemonic, Int32 opcode, Int32 function)
            => new NativeInstruction(
                mnemonic: mnemonic,
                operand_format: new OperandFormat(REGISTER_TYPE, REGISTER_TYPE, IMMEDIATE_TYPE),
                mapping: GetRIBuilder()
                    .Opcode(opcode)
                    .Function(function)
                    .Build()
            );

        // Instruction Building Functions /////////////////////////////////////

        /// <summary>
        ///     Register all 3 formats of the AND/NAND/OR/NOR/XOR/XNOR/ADD/SUB instructions.
        /// </summary>
        private static void RegisterALUInstructions([NotNull] InstructionRegistry registry) {

            const Int32 ALU_RR_OPCODE = 0;
            const Int32 ALU_RI_OPCODE = 1;
            const Int32 ALU_RIC_OPCODE = 2;

            Tuple<string, Int32>[] alu_instructions = {
                /* Mnemonic, Function */
                new Tuple<string, Int32>("and", 0),
                new Tuple<string, Int32>("nand", 1),
                new Tuple<string, Int32>("or", 2),
                new Tuple<string, Int32>("nor", 3),
                new Tuple<string, Int32>("xor", 4),
                new Tuple<string, Int32>("xnor", 5),
                new Tuple<string, Int32>("add", 6),
                new Tuple<string, Int32>("sub", 7),
            };

            // Add RR, RI, and RIC formats for each instruction.
            foreach (var pair in alu_instructions) {
                registry.Register(CreateRRInstruction(pair.Item1, ALU_RR_OPCODE, pair.Item2));
                registry.Register(CreateRIInstruction(pair.Item1, ALU_RI_OPCODE, pair.Item2));
                registry.Register(CreateRIInstruction(pair.Item1 + 'c', ALU_RIC_OPCODE, pair.Item2));
            }
        }

        /// <summary>
        ///     Register both formats of the SLL/SRL/SRA/ROL/ROR instructions.
        /// </summary>
        private static void RegisterShifterInstructions([NotNull] InstructionRegistry registry) {

            const Int32 SHIFTER_RR_OPCODE = 3;
            const Int32 SHIFTER_RI_OPCODE = 4;

            Tuple<string, Int32>[] shifter_instructions = {
                /* Mnemonic, Function */
                new Tuple<string, Int32>("sll", 0),
                new Tuple<string, Int32>("srl", 1),
                new Tuple<string, Int32>("sra", 2),
                new Tuple<string, Int32>("rol", 3),
                new Tuple<string, Int32>("ror", 4),
            };

            // Add RR and RI formats for each instruction.
            foreach (var pair in shifter_instructions) {
                registry.Register(CreateRRInstruction(pair.Item1, SHIFTER_RR_OPCODE, pair.Item2));
                registry.Register(CreateRIInstruction(pair.Item1, SHIFTER_RI_OPCODE, pair.Item2));
            }

        }

        /// <summary>
        ///     Register both formats of the CEQ/CNEQ/CLT/CLTE/CLTU/CLTEU/CGT/CGTU instructions.
        /// </summary>
        private static void RegisterComparisonInstructions([NotNull] InstructionRegistry registry) {

            const Int32 COMPARATOR_RR_OPCODE = 5;
            const Int32 COMPARATOR_RI_OPCODE = 6;

            Tuple<string, Int32>[] comparison_instructions = {
                /* Mnemonic, Function */
                new Tuple<string, Int32>("ceq", 0),
                new Tuple<string, Int32>("cneq", 1),
                new Tuple<string, Int32>("clt", 2),
                new Tuple<string, Int32>("clte", 3),
                new Tuple<string, Int32>("cltu", 4),
                new Tuple<string, Int32>("clteu", 5),
                new Tuple<string, Int32>("cgt", 6),
                new Tuple<string, Int32>("cgtu", 7),
            };

            // Add RR and RI formats for each instruction.
            foreach (var pair in comparison_instructions) {
                registry.Register(CreateRRInstruction(pair.Item1, COMPARATOR_RR_OPCODE, pair.Item2));
                registry.Register(CreateRIInstruction(pair.Item1, COMPARATOR_RI_OPCODE, pair.Item2));
            }
        }

        /// <summary>
        ///     Register all 3 formats of the LB/LBU/LH/SB/SH instructions.
        ///     TODO
        /// </summary>
        private static void RegisterMemoryInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register the LI/LIC/LA/LAC/MOV/MOVC instructions.
        ///     TODO
        /// </summary>
        /// <param name="registry"> </param>
        private static void RegisterDataTransferInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register both formats of the JMP/BRC instructions.
        ///     TODO
        /// </summary>
        /// <param name="registry"> </param>
        private static void RegisterControlTransferInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register the following instructions:
        ///     - HALT
        /// </summary>
        private static void RegisterMiscInstructions([NotNull] InstructionRegistry registry) {
            registry.Register(
                new NativeInstruction(
                    mnemonic: "halt",
                    operand_format: new OperandFormat(),
                    mapping: new InstructionFieldMappingBuilder()
                        .Opcode(31)
                        .Function(0)
                        .R1(0)
                        .R2(0)
                        .Immediate(0)
                        .Build())
            );
        }

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Get the list of all registered instructions
        ///     which are valid for the architecture.
        /// </summary>
        [NotNull]
        public static InstructionRegistry GetRegistry()
            => INSTRUCTION_REGISTRY;
    }
}