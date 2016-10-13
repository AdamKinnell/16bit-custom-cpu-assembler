using System;
using JetBrains.Annotations;

namespace Assembler.Splitter {
    /// <summary>
    ///     Represents the untokenized components of a source line.
    /// </summary>
    public class SourceLine {

        // Static Functions ///////////////////////////////////////////////////

        [NotNull]
        public static SourceLine CreateEmpty() => new SourceLine(null, null, null, null);

        // Constructors ///////////////////////////////////////////////////////

        public SourceLine([CanBeNull] string label,
                          [CanBeNull] string mnemonic,
                          [CanBeNull] string operands,
                          [CanBeNull] string comment) {

            // Perform sanity checks.
            if ((mnemonic == null) && (operands != null)) {
                throw new ArgumentException("Cannot have operands without mnemonic.", nameof(operands));
            }

            Label = label;
            Mnemonic = mnemonic;
            Operands = operands;
            Comment = comment;
        }

        // Properties /////////////////////////////////////////////////////////
        [CanBeNull] public string Label { get; }
        [CanBeNull] public string Mnemonic { get; }
        [CanBeNull] public string Operands { get; }
        [CanBeNull] public string Comment { get; }

        public bool HasInstruction => Mnemonic != null;
        public bool HasComment => Comment != null;
        public bool HasLabel => Label != null;
        public bool IsEmpty => (Label ?? Mnemonic ?? Operands ?? Comment) == null;

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is SourceLine && Equals((SourceLine) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] SourceLine other) =>
            String.Equals(Label, other.Label) &&
            String.Equals(Mnemonic, other.Mnemonic) &&
            String.Equals(Operands, other.Operands) &&
            String.Equals(Comment, other.Comment);

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}