using System;
using Assembler.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Operands.Parsers {

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
            var token =
               (IOperand) new BaseOffsetOperandParser().TryParse(operand) ??
               (IOperand) new ImmediateOperandParser().TryParse(operand) ??
               (IOperand) new RegisterOperandParser().TryParse(operand);

            if (token != null)
                return token;
            else
                throw new ArgumentException("Could not be parsed as operand.", nameof(operand));
        }
    }
}