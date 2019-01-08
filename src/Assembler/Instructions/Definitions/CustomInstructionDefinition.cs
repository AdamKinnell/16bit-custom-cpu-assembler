using System;
using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions.Definitions {

    /// <summary>
    ///     Defines a custom instruction with a name (mnemonic),
    ///     operand format, and delegated assembly logic.
    /// </summary>
    public class CustomInstructionDefinition : IInstructionDefinition {

        // Fields /////////////////////////////////////////////////////////////

        [NotNull] private readonly Func<OperandList, MachineCode> assembler_delegate;

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from an instruction format and assembler delegate.
        /// </summary>
        /// <param name="format"> The format of this pseudo-instruction. </param>
        /// <param name="assembler_delegate">
        ///     A function that will assemble machine code based on a list of operands.
        ///     Must not return null.
        /// </param>
        public CustomInstructionDefinition([NotNull] InstructionFormat format,
                                           [NotNull] Func<OperandList, MachineCode> assembler_delegate) {

            Format = format;
            this.assembler_delegate = assembler_delegate;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <inheritdoc />
        public InstructionFormat Format { get; }

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Assemble this instruction with the given operands.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If the format of the operands given is different to the format expected.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     If the assembler delegate returns null.
        /// </exception>
        public MachineCode AssembleWithOperands(OperandList operands) {
            if (operands.Format.Equals(Format.OperandFormat)) {
                var machine_code = assembler_delegate(operands);
                if (machine_code == null)
                    throw new InvalidOperationException("The assembler delegate returned null.");
                else
                    return machine_code;
            } else
                throw new ArgumentException("The given operands are not in the expected format.");
        }

    }
}