using System;
using Assembler.Instructions.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Parser.Operand {

    /// <summary>
    ///     Parses an instruction operand as specified in the source code.
    /// </summary>
    class OperandParser {

        /// <summary>
        ///     Parse the given string as a single instruction operand.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If could not parse as operand.
        /// </exception>
        [NotNull]
        public IOperand Parse([NotNull] string operand) {
            try {
                return new ImmediateOperandParser().Parse(operand);
            } catch (ArgumentException) {}
            try {
                return new RegisterOperandParser().Parse(operand);
            } catch (ArgumentException) {}

            throw new ArgumentException("Could not be parsed as operand.", nameof(operand));
        }
    }
}