﻿using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Represents an instruction that can be
    ///     be natively executed by the architecture.
    /// </summary>
    public class NativeInstruction {

        // Fields /////////////////////////////////////////////////////////////

        [NotNull] private InstructionFieldMappingBuilder.InstructionFieldMapping field_mapping;

        // Constructors ///////////////////////////////////////////////////////

        public NativeInstruction([NotNull] string mnemonic, [NotNull] OperandFormat format,
                                 [NotNull] InstructionFieldMappingBuilder.InstructionFieldMapping mapping) {
            Mnemonic = mnemonic;
            OperandFormat = format;
            field_mapping = mapping;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary> The mnemonic for this instruction. </summary>
        [NotNull] public string Mnemonic { get; }

        /// <summary> The format of this instruction's operands. </summary>
        [NotNull] public OperandFormat OperandFormat { get; }

        // Implemented Functions //////////////////////////////////////////////

        // Functions //////////////////////////////////////////////////////////
    }
}