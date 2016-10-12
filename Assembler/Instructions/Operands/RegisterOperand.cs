using System;

namespace Assembler.Instructions.Operands {

    /// <summary>
    ///     Represents an operand specifying a register.
    /// </summary>
    class RegisterOperand {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct a new register type operand.
        /// </summary>
        /// <param name="register_number"> Must be within range 0-15 </param>
        public RegisterOperand(int register_number) {
            if ((register_number < 0) || (register_number > 15))
                throw new ArgumentException("Must be in range 0-15", nameof(register_number));

            RegisterNumber = register_number;
        }

        // Properties /////////////////////////////////////////////////////////

        public int RegisterNumber { get; }
    }
}