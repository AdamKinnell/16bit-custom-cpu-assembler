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
        ///     Attempt to parse the given register specification string as an operand.
        /// </summary>
        /// <param name="register"> </param>
        /// <returns> Null if no valid register is specified. </returns>
        [CanBeNull]
        public RegisterOperand TryParse([NotNull] string register) {
            if (register.First() == '$') {
                register = register.Substring(1); // Remove '$'
                return TryParseAsNumber(register) ??
                    TryParseAsName(register);
            } else {
                return null;
            }
        }
    }
}