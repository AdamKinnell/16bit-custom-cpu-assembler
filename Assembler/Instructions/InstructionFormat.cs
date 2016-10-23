using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Represents the expected or given format of an instruction.
    /// </summary>
    public class InstructionFormat {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from a mnemonic and operand format.
        /// </summary>
        public InstructionFormat([NotNull] string mnemonic, [NotNull] OperandFormat operand_format) {
            Mnemonic = mnemonic;
            OperandFormat = operand_format;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary> The mnemonic of the instruction. </summary>
        [NotNull] public string Mnemonic { get; }

        /// <summary> The format of operands for the instruction. </summary>
        [NotNull] public OperandFormat OperandFormat { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is InstructionFormat && Equals((InstructionFormat) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] InstructionFormat other) =>
            Equals(Mnemonic, other.Mnemonic) &&
            Equals(OperandFormat, other.OperandFormat);

        /// <inheritdoc />
        public override int GetHashCode() {
            unchecked {
                return (Mnemonic.GetHashCode()*397) ^ OperandFormat.GetHashCode();
            }
        }

    }
}