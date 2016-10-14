using System;
using System.Linq;
using Assembler.Constants;
using Assembler.Instructions.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Parser.Operand {

    /// <summary>
    ///     Parses a register operand as specified in the source code.
    /// </summary>
    class RegisterOperandParser {

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Attempt to parse the given register specification as a number.
        /// </summary>
        /// <returns> Null if the register could not be parsed as a number. </returns>
        [CanBeNull]
        private RegisterOperand TryParseAsNumber([NotNull] string register) {
            int num;
            if (int.TryParse(register, out num) &&
                Registers.IsValidRegisterNumber(num)) {
                return new RegisterOperand(num);
            } else {
                return null;
            }
        }

        /// <summary>
        ///     Attempt to parse the given register specification as a name.
        /// </summary>
        /// <returns> Null if the register could not be paarsed as a name. </returns>
        [CanBeNull]
        private RegisterOperand TryParseAsName([NotNull] string register) {
            var num = Registers.NameToNumber(register);
            return num == null
                ? null
                : new RegisterOperand(num.Value);
        }

        /// <summary>
        ///     Parse the given register specification string as an operand.
        /// </summary>
        /// <param name="register"> Should start with '$' </param>
        /// <exception cref="ArgumentException"> If no valid register is specified. </exception>
        [NotNull]
        public RegisterOperand Parse([NotNull] string register) {

            // Ensure we've been given a register.
            if (register.First() != '$')
                throw new ArgumentException("No register specified; needs '$'.", nameof(register));
            else
                register = register.Substring(1); // Remove '$'

            // Attempt to resolve register number.
            RegisterOperand operand;
            if ((operand = TryParseAsNumber(register)) != null)
                return operand;
            if ((operand = TryParseAsName(register)) != null)
                return operand;

            // Invalid; could not parse register.
            throw new ArgumentException("The specified register does not exist.", nameof(register));
        }
    }
}