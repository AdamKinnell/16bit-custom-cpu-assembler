using System;
using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions.Definitions {

    /// <summary>
    ///     Represents an instruction format and it's mapping to machine code.
    /// </summary>
    public interface IInstructionDefinition {

        // Properties /////////////////////////////////////////////////////////

        /// <summary> The format of this instruction. </summary>
        [NotNull] InstructionFormat Format { get; }

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Assemble this instruction with the given operands.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If the format of the operands given is different to the format expected.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     If a logic error occurs.
        /// </exception>
        [NotNull]
        MachineCode AssembleWithOperands([NotNull] OperandList operands);
    }
}