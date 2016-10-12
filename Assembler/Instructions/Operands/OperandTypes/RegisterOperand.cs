using System;

namespace Assembler.Instructions.Operands.OperandTypes {

    /// <summary>
    ///     Represents an operand specifying a register.
    /// </summary>
    public class RegisterOperand {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct a new register operand from it's number.
        /// </summary>
        /// <param name="register_number"> Must be valid. </param>
        /// <exception cref="ArgumentException">If number is invalid.</exception>
        public RegisterOperand(int register_number) {
            if (!Constants.Registers.IsValidRegisterNumber(register_number))
                throw new ArgumentException("Not a valid register number.", nameof(register_number));
            
            RegisterNumber = register_number;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The lower 4 bits specify the register number.
        /// </summary>
        public Int32 RegisterNumber { get; }
    }
}