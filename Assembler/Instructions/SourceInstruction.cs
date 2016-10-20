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
        ///     Construct from mnemonic and operand list.
        /// </summary>
        public SourceInstruction([NotNull] string mnemonic, [CanBeNull] OperandList operands) {
            Mnemonic = mnemonic;
            Operands = operands ?? OperandList.CreateEmpty();
        }

        /// <summary>
        ///     Construct from mnemonic and operands.
        /// </summary>
        public SourceInstruction([NotNull] string mnemonic, [NotNull] params IOperand[] operands) {
            Mnemonic = mnemonic;
            Operands = new OperandList(operands);
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary> The mnemonic given for this instruction. </summary>
        [NotNull] public string Mnemonic { get; }

        /// <summary> The operands given for this instruction. </summary>
        [NotNull] public OperandList Operands { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is SourceInstruction && Equals((SourceInstruction) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] SourceInstruction other) =>
            Equals(Mnemonic, other.Mnemonic) &&
            Equals(Operands, other.Operands);

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}