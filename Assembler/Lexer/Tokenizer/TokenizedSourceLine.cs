using System;
using Assembler.Instructions;
using Assembler.Label;
using JetBrains.Annotations;

namespace Assembler.Lexer.Tokenizer {

    /// <summary>
    ///     Represents the logical components of a source line in a tokenized format.
    /// </summary>
    public class TokenizedSourceLine {

        // Static Functions ///////////////////////////////////////////////////

        [NotNull]
        public static TokenizedSourceLine CreateEmpty()
            => new TokenizedSourceLine();

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct with optional label definition and instruction.
        /// </summary>
        public TokenizedSourceLine([CanBeNull] LabelDefinition label = null,
                                   [CanBeNull] SourceInstruction instruction = null) {
            Label = label;
            Instruction = instruction;
        }

        /// <summary>
        ///     Construct with only a label.
        /// </summary>
        public TokenizedSourceLine([NotNull] LabelDefinition label)
            : this(label, null) {}

        /// <summary>
        ///     Construct with only an instruction.
        /// </summary>
        public TokenizedSourceLine([NotNull] SourceInstruction instruction)
            : this(null, instruction) {}

        // Properties /////////////////////////////////////////////////////////

        [CanBeNull] public LabelDefinition Label { get; }
        [CanBeNull] public SourceInstruction Instruction { get; }

        public bool HasLabel => Label != null;
        public bool HasInstruction => Instruction != null;
        public bool IsEmpty => !HasLabel && !HasInstruction;

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is TokenizedSourceLine && Equals((TokenizedSourceLine) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] TokenizedSourceLine other) =>
            Equals(Label, other.Label) &&
            Equals(Instruction, other.Instruction);

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}