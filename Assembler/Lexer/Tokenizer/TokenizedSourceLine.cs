using System;
using Assembler.Instructions;
using JetBrains.Annotations;

namespace Assembler.Lexer.Tokenizer {

    /// <summary>
    ///     Represents the logical components of a source line in a tokenized format.
    /// </summary>
    public class TokenizedSourceLine {

        // Static Functions ///////////////////////////////////////////////////

        [NotNull]
        public static TokenizedSourceLine CreateEmpty() => new TokenizedSourceLine(null, null, null);

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct with all components.
        /// </summary>
        /// <param name="label"> </param>
        /// <param name="instruction"> </param>
        /// <param name="comment"> </param>
        public TokenizedSourceLine([CanBeNull] string label,
                                   [CanBeNull] SourceInstruction instruction,
                                   [CanBeNull] string comment) {
            Label = label;
            Instruction = instruction;
            Comment = comment;
        }

        /// <summary>
        ///     Construct just from an instruction.
        /// </summary>
        public TokenizedSourceLine([NotNull] SourceInstruction instruction)
            : this(null, instruction, null) {}

        // Properties /////////////////////////////////////////////////////////

        [CanBeNull] public string Label { get; }
        [CanBeNull] public SourceInstruction Instruction { get; }
        [CanBeNull] public string Comment { get; }

        public bool HasLabel => Label != null;
        public bool HasInstruction => Instruction != null;
        public bool HasComment => Comment != null;
        public bool IsEmpty => !HasLabel && !HasInstruction && !HasComment;

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is TokenizedSourceLine && Equals((TokenizedSourceLine) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] TokenizedSourceLine other) =>
            Equals(Label, other.Label) &&
            Equals(Instruction, other.Instruction) &&
            Equals(Comment, other.Comment);

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}