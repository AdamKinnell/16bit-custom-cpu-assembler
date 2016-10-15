using JetBrains.Annotations;

namespace Assembler.Instructions.Operands.Types {

    /// <summary>
    ///     Represents an operand specifying a base address plus an offset.
    /// </summary>
    public class BaseOffsetOperand : IOperand {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct a new base-offset operand from register and immediate operands.
        /// </summary>
        public BaseOffsetOperand([NotNull] RegisterOperand @base, [NotNull] ImmediateOperand offset) {
            Base = @base;
            Offset = offset;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The register specifying some base address.
        /// </summary>
        public RegisterOperand Base { get; }

        /// <summary>
        ///     The immediate specifying some offset from the base address.
        /// </summary>
        public ImmediateOperand Offset { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is BaseOffsetOperand && Equals((BaseOffsetOperand) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] BaseOffsetOperand other)
            => Equals(Base, other.Base) && Equals(Offset, other.Offset);

        /// <inheritdoc />
        public override int GetHashCode() {
            unchecked {
                return ((Base?.GetHashCode() ?? 0)*397) ^ (Offset?.GetHashCode() ?? 0);
            }
        }
    }
}