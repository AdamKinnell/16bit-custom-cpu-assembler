using System;
using JetBrains.Annotations;

namespace Assembler.Operands.Types {

    /// <summary>
    ///     Represents an operand specifying an immediate compile-time value.
    /// </summary>
    public class ImmediateOperand : IOperand {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct a new immediate operand from it's value.
        /// </summary>
        /// <param name="value"> No range limitations. </param>
        public ImmediateOperand(int value) {
            Value = value;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The lower 16 bits specify the immediate value.
        /// </summary>
        public Int32 Value { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is ImmediateOperand && Equals((ImmediateOperand) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] ImmediateOperand other)
            => Value == other.Value;

        /// <inheritdoc />
        public override int GetHashCode() => Value;
    }
}