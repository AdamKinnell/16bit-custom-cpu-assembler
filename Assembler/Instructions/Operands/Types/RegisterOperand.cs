using System;
using Assembler.Constants;
using JetBrains.Annotations;

namespace Assembler.Instructions.Operands.Types {

    /// <summary>
    ///     Represents an operand specifying a register.
    /// </summary>
    public class RegisterOperand : IOperand {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct a new register operand from it's number.
        /// </summary>
        /// <param name="register_number"> Must be valid. </param>
        /// <exception cref="ArgumentException"> If number is invalid. </exception>
        public RegisterOperand(int register_number) {
            if (!Registers.IsValidRegisterNumber(register_number))
                throw new ArgumentException("Not a valid register number.", nameof(register_number));

            RegisterNumber = register_number;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The lower 4 bits specify the register number.
        /// </summary>
        public Int32 RegisterNumber { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is RegisterOperand && Equals((RegisterOperand) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] RegisterOperand other)
            => RegisterNumber == other.RegisterNumber;

        /// <inheritdoc />
        public override int GetHashCode() => RegisterNumber;
    }
}