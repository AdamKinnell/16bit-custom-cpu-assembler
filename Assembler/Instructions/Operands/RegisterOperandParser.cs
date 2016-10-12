using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Assembler.Instructions.Operands {

    /// <summary>
    ///     Parses a register operand as specified in the source code.
    /// </summary>
    class RegisterOperandParser {

        // Static Fields //////////////////////////////////////////////////////

        private static readonly Dictionary<string, int> REGISTER_NAME_MAPPINGS =
            new Dictionary<string, int> {
                // TODO
                { "zero", 0}
            };

        // Functions //////////////////////////////////////////////////////////

        [NotNull]
        public RegisterOperand Parse([NotNull] string register) {
            if (register.First() != '$')
                throw new ArgumentException("Argument is not a register.", nameof(register));
            else
                register = register.Substring(1); // Remove '$'

            // Check if register specified as number.
            var number = -1;
            if (int.TryParse(register, out number)) {
                return new RegisterOperand(number);
            }

            // Check if register specified as name.
            if (REGISTER_NAME_MAPPINGS.ContainsKey(register)) {
                return new RegisterOperand(REGISTER_NAME_MAPPINGS[register]);
            }

            // Invalid register specification.
            throw new ArgumentException("The specified register does not exist.", nameof(register));

        }
    }
}