using System;
using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    /// 
    /// </summary>
    class ArchitectureInstruction {

        // Fields /////////////////////////////////////////////////////////////

        // Constructors ///////////////////////////////////////////////////////

        public ArchitectureInstruction([NotNull] string mnemonic, [NotNull] OperandFormat operand_format,
                                       [NotNull] AssemblerMappingBuilder.AssemblerMapping mapping) {
            Mnemonic = mnemonic;
            OperandFormat = operand_format;
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