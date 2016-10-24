using System;
using Assembler.Instructions.Definitions;
using Assembler.Registries;
using JetBrains.Annotations;

namespace Assembler.Constants {

    /// <summary>
    ///     Contains definitions of all native instructions in the architecture.
    /// </summary>
    public class Instructions {

        // Static Fields //////////////////////////////////////////////////////

        private static readonly InstructionRegistry INSTRUCTION_REGISTRY = new InstructionRegistry();

        // Static Constructors ////////////////////////////////////////////////

        static Instructions() {
            // Native
            RegisterALUInstructions(INSTRUCTION_REGISTRY);
            RegisterShifterInstructions(INSTRUCTION_REGISTRY);
            RegisterComparisonInstructions(INSTRUCTION_REGISTRY);
            RegisterMemoryLoadInstructions(INSTRUCTION_REGISTRY);
            RegisterMemoryStoreInstructions(INSTRUCTION_REGISTRY);
            RegisterMiscInstructions(INSTRUCTION_REGISTRY);
            // Pseudo
            RegisterDataTransferPseudoInstructions(INSTRUCTION_REGISTRY);
            RegisterControlTransferPseudoInstructions(INSTRUCTION_REGISTRY);
        }

        // Static Functions////////////////////////////////////////////////////

        /// <summary>
        ///     Register RR,RI,RI(C) formats of the AND/NAND/OR/NOR/XOR/XNOR/ADD/SUB instructions.
        /// </summary>
        private static void RegisterALUInstructions([NotNull] InstructionRegistry registry) {

            const Int32 ALU_RR_OPCODE = 0;
            const Int32 ALU_RI_OPCODE = 1;
            const Int32 ALU_RIC_OPCODE = 2;

            Tuple<string, Int32>[] alu_instructions = {
                /* (Mnemonic, Function) */
                new Tuple<string, Int32>("and", 0),
                new Tuple<string, Int32>("nand", 1),
                new Tuple<string, Int32>("or", 2),
                new Tuple<string, Int32>("nor", 3),
                new Tuple<string, Int32>("xor", 4),
                new Tuple<string, Int32>("xnor", 5),
                new Tuple<string, Int32>("add", 6),
                new Tuple<string, Int32>("sub", 7),
            };

            // Add RR, RI, and RI(C) formats for each instruction.
            foreach (var pair in alu_instructions) {
                registry.Register(InstructionDefinitionFactory.CreateRRInstruction(
                                      pair.Item1, ALU_RR_OPCODE, pair.Item2)); ///////// ADD $t0, $t1     :=: $t0 += $t1
                registry.Register(InstructionDefinitionFactory.CreateRIStandardInstruction(
                                      pair.Item1, ALU_RI_OPCODE, pair.Item2)); ///////// ADD $t0, $t1, 8  :=: $t0 = $t1 + 8
                registry.Register(InstructionDefinitionFactory.CreateRIImplicitDestinationInstruction(
                                      pair.Item1, ALU_RI_OPCODE, pair.Item2)); ///////// ADD $t0, 8       :=: $t0 += 8 
                registry.Register(InstructionDefinitionFactory.CreateRIStandardInstruction(
                                      pair.Item1 + 'c', ALU_RIC_OPCODE, pair.Item2)); // ADDC $t0, $t1, 8 :=: if(c) $t0 = $t1 + 8
                registry.Register(InstructionDefinitionFactory.CreateRIImplicitDestinationInstruction(
                                      pair.Item1 + 'c', ALU_RIC_OPCODE, pair.Item2)); // ADDC $t0, 8      :=: if(c) $t0 += 8
            }
        }

        /// <summary>
        ///     Register RR and RI formats of the SLL/SRL/SRA/ROL/ROR instructions.
        /// </summary>
        private static void RegisterShifterInstructions([NotNull] InstructionRegistry registry) {

            const Int32 SHIFTER_RR_OPCODE = 3;
            const Int32 SHIFTER_RI_OPCODE = 4;

            Tuple<string, Int32>[] shifter_instructions = {
                /* (Mnemonic, Function) */
                new Tuple<string, Int32>("sll", 0),
                new Tuple<string, Int32>("srl", 1),
                new Tuple<string, Int32>("sra", 2),
                new Tuple<string, Int32>("rol", 3),
                new Tuple<string, Int32>("ror", 4),
            };

            // Add RR and RI formats for each instruction.
            foreach (var pair in shifter_instructions) {
                registry.Register(InstructionDefinitionFactory.CreateRRInstruction(
                                      pair.Item1, SHIFTER_RR_OPCODE, pair.Item2)); // SLL $t0, $t1    :=: $t0 << $t1
                registry.Register(InstructionDefinitionFactory.CreateRIStandardInstruction(
                                      pair.Item1, SHIFTER_RI_OPCODE, pair.Item2)); // SLL $t0, $t1, 8 :=: $t0 = $t1 << 8
                registry.Register(InstructionDefinitionFactory.CreateRIImplicitDestinationInstruction(
                                      pair.Item1, SHIFTER_RI_OPCODE, pair.Item2)); // SLL $t0, 8      :=: $t0 <<= 8
            }

        }

        /// <summary>
        ///     Register RR and RI formats of the CEQ/CNEQ/CLT/CLTE/CLTU/CLTEU/CGT/CGTU instructions.
        /// </summary>
        private static void RegisterComparisonInstructions([NotNull] InstructionRegistry registry) {

            const Int32 COMPARATOR_RR_OPCODE = 5;
            const Int32 COMPARATOR_RI_OPCODE = 6;

            Tuple<string, Int32>[] comparison_instructions = {
                /* (Mnemonic, Function) */
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
                registry.Register(InstructionDefinitionFactory.CreateRRInstruction(
                                      pair.Item1, COMPARATOR_RR_OPCODE, pair.Item2)); // CEQ $t0, $t1 :=: c = ($t0 == $t1)
                registry.Register(InstructionDefinitionFactory.CreateRIDestinationRegisterUnusedInstruction(
                                      pair.Item1, COMPARATOR_RI_OPCODE, pair.Item2)); // CEQ $t0, 8   :=: c = ($t0 == 8)
            }
        }

        /// <summary>
        ///     Register: Register address, Immediate address, and Base-Offset addressing formats of the LB/LBU/LH instructions.
        /// </summary>
        private static void RegisterMemoryLoadInstructions([NotNull] InstructionRegistry registry) {
            const Int32 MEMORY_LOAD_OPCODE = 7;

            Tuple<string, Int32>[] memory_load_instructions = {
                /* (Mnemonic, Function) */
                new Tuple<string, Int32>("lb", 3),
                new Tuple<string, Int32>("lbu", 4),
                new Tuple<string, Int32>("lh", 1),
            };

            // Add Register, Immediate, and BaseOffset formats for each instruction.
            foreach (var pair in memory_load_instructions) {
                registry.Register(InstructionDefinitionFactory.CreateRRInstruction(
                                      pair.Item1, MEMORY_LOAD_OPCODE, pair.Item2)); // LH $t0, $t1       :=: $t0 = *($t1)
                registry.Register(InstructionDefinitionFactory.CreateRISourceRegisterUnusedInstruction(
                                      pair.Item1, MEMORY_LOAD_OPCODE, pair.Item2)); // LH $t0, 0xFF      :=: $t0 = *(0xFF)
                registry.Register(InstructionDefinitionFactory.CreateBaseOffsetInstruction(
                                      pair.Item1, MEMORY_LOAD_OPCODE, pair.Item2)); // LH $t0, 0xFF($t1) :=: $t0 = *($t1 + 0xFF)
            }
        }

        /// <summary>
        ///     Register: Register address, Immediate address, and Base-Offset addressing formats of the SB/SH instructions.
        /// </summary>
        private static void RegisterMemoryStoreInstructions([NotNull] InstructionRegistry registry) {
            const Int32 MEMORY_STORE_OPCODE = 8;

            Tuple<string, Int32>[] memory_store_instructions = {
                /* (Mnemonic, Function) */
                new Tuple<string, Int32>("sb", 7),
                new Tuple<string, Int32>("sh", 6),
            };

            // Add Register, Immediate, and BaseOffset formats for each instruction.
            foreach (var pair in memory_store_instructions) {
                registry.Register(InstructionDefinitionFactory.CreateRRInstruction(
                                      pair.Item1, MEMORY_STORE_OPCODE, pair.Item2)); // SH $t0, $t1       :=: *($t1) = $t0
                registry.Register(InstructionDefinitionFactory.CreateRISourceRegisterUnusedInstruction(
                                      pair.Item1, MEMORY_STORE_OPCODE, pair.Item2)); // SH $t0, 0xFF      :=: *(0xFF) = $t0
                registry.Register(InstructionDefinitionFactory.CreateBaseOffsetInstruction(
                                      pair.Item1, MEMORY_STORE_OPCODE, pair.Item2)); // SH $t0, 0xFF($t1) :=: *($t1 + 0xFF) = $t0
            }
        }

        /// <summary>
        ///     Register the LI/LIC/LA/LAC/MOV/MOVC pseudo instructions.
        /// </summary>
        private static void RegisterDataTransferPseudoInstructions([NotNull] InstructionRegistry registry) {

            // LI $t0, 8 :=: $t0 = 8
            registry.Register(
                InstructionDefinitionFactory.CreateRISourceRegisterUnusedInstruction(
                    "li", 1, 2)); // OR $t0, $zero, 8

            // LIC $t0, 8 :=: if(c) $t0 = 8
            registry.Register(
                InstructionDefinitionFactory.CreateRISourceRegisterUnusedInstruction(
                    "lic", 2, 2)); // ORC $t0, $zero, 8

            // LA $t0, 8 :=: $t0 = 8
            registry.Register(
                InstructionDefinitionFactory.CreateRISourceRegisterUnusedInstruction(
                    "la", 1, 2)); // OR $t0, $zero, 8

            // LAC $t0, 8 :=: if(c) $t0 = 8
            registry.Register(
                InstructionDefinitionFactory.CreateRISourceRegisterUnusedInstruction(
                    "lac", 2, 2)); // ORC $t0, $zero, 8

            // MOV $t0, $t1 :=: $t0 = $t1
            registry.Register(
                InstructionDefinitionFactory.CreateRRInstruction(
                    "la", 1, 2)); // OR $t0, $t1, 0

            // MOVC $t0, $t1 :=: if(c) $t0 = $t1
            registry.Register(
                InstructionDefinitionFactory.CreateRRInstruction(
                    "lac", 2, 2)); // ORC $t0, $t1, 0
        }

        /// <summary>
        ///     Register both formats of the JMP/BRC instructions.
        ///     TODO
        /// </summary>
        private static void RegisterControlTransferPseudoInstructions([NotNull] InstructionRegistry registry) {

            //// JMP 0x8 :=: goto 0x8
            //registry.Register(
            //    InstructionDefinitionFactory.CreateRRInstruction(
            //        "jmp", 1, 2)); // OR $pc, $zero, 0x8

            //// JMP $t0 :=: goto $t0
            //registry.Register(
            //    InstructionDefinitionFactory.CreateRRInstruction(
            //        "jmp", 1, 2)); // OR $pc, $t0, 0

            //// BRC 0x8 :=: if(c) goto 0x8
            //registry.Register(
            //    InstructionDefinitionFactory.CreateRRInstruction(
            //        "brc", 1, 2)); // ORC $pc, $zero, 0x8

            //// BRC $t0 :=: if(c) goto $t0
            //registry.Register(
            //    InstructionDefinitionFactory.CreateRRInstruction(
            //        "brc", 1, 2)); // ORC $pc, $t0, 0
        }

        /// <summary>
        ///     Register the following instructions:
        ///     - HALT
        /// </summary>
        private static void RegisterMiscInstructions([NotNull] InstructionRegistry registry) {
            registry.Register(
                InstructionDefinitionFactory.CreateOpcodeOnlyInstruction(
                    "halt", 31));
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