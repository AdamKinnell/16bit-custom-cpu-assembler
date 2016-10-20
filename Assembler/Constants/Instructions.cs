﻿using System;
using Assembler.Instructions;
using Assembler.Operands;
using Assembler.Operands.Types;
using Assembler.Registries;
using JetBrains.Annotations;

namespace Assembler.Constants {

    /// <summary>
    ///     Contains definitions of all native instructions in the architecture.
    /// </summary>
    class Instructions {

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

        // Static Functions ///////////////////////////////////////////////////

        /// <summary>
        ///     Register all 3 formats of the AND/NAND/OR/NOR/XOR/XNOR/ADD/SUB instructions.
        /// </summary>
        private static void RegisterALUInstructions([NotNull] InstructionRegistry registry) {
            registry.Register(
                new ArchitectureInstruction(
                    mnemonic: "and",
                    operand_format: new OperandFormat(REGISTER_TYPE, REGISTER_TYPE),
                    mapping: new AssemblerMappingBuilder()
                        .Opcode(_ => 0)
                        .Function(_ => 0)
                        .R1(_ => 1)
                        .R2(_ => 1)
                        .Immediate(_ => 0)
                        .Build()
                ));
        }

        /// <summary>
        ///     Register both formats of the SLL/SRL/SRA/ROL/ROR instructions.
        /// </summary>
        private static void RegisterShifterInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register both formats of the CEQ/CNEQ/CLT/CLTE/CLTEU/CGT/CGTU instructions.
        /// </summary>
        private static void RegisterComparisonInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register all 3 formats of the LB/LBU/LH/SB/SH instructions.
        /// </summary>
        private static void RegisterMemoryInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register the LI/LIC/LA/LAC/MOV/MOVC instructions.
        /// </summary>
        /// <param name="registry"> </param>
        private static void RegisterDataTransferInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register both formats of the JMP/BRC instructions.
        /// </summary>
        /// <param name="registry"> </param>
        private static void RegisterControlTransferInstructions([NotNull] InstructionRegistry registry) {}

        /// <summary>
        ///     Register the following instructions:
        ///     - HALT
        /// </summary>
        private static void RegisterMiscInstructions([NotNull] InstructionRegistry registry) {}
    }
}