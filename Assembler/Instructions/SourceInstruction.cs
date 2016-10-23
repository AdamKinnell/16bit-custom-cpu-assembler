using System;
using Assembler.Operands;
using Assembler.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Represents an instruction as specified in the source file
    ///     which may or may not be valid for the architecture.
    /// </summary>
    public class SourceInstruction {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from format and operand list.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// If the format of the operands given is different from
        /// </exception>
        public SourceInstruction([NotNull] InstructionFormat format, [NotNull] OperandList operands) {
            if (!format.OperandFormat.Equals(operands.Format))
                throw new ArgumentException("Operand formats do not match.");

            Format = format;
            Operands = operands;
        }

        /// <summary>
        ///     Construct from mnemonic and operand list.
        /// </summary>
        public SourceInstruction([NotNull] string mnemonic, [NotNull] OperandList operands)
            : this(new InstructionFormat(mnemonic, operands.Format), operands) {}

        /// <summary>
        ///     Construct from mnemonic and operands.
        /// </summary>
        public SourceInstruction([NotNull] string mnemonic, [NotNull] params IOperand[] operands)
            : this(mnemonic, new OperandList(operands)) {}

        // Properties /////////////////////////////////////////////////////////

        /// <summary> The format of this instruction. </summary>
        [NotNull] public InstructionFormat Format { get; }

        /// <summary> The operands given for this instruction. </summary>
        [NotNull] public OperandList Operands { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is SourceInstruction && Equals((SourceInstruction) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] SourceInstruction other) =>
            Equals(Format, other.Format) &&
            Equals(Operands, other.Operands);

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}