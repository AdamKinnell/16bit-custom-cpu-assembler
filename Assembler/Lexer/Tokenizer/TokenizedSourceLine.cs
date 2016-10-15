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
        public static TokenizedSourceLine CreateEmpty() => new TokenizedSourceLine(null, null);

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct with all components.
        /// </summary>
        /// <param name="label"> </param>
        /// <param name="instruction"> </param>
        public TokenizedSourceLine([CanBeNull] string label,
                                   [CanBeNull] SourceInstruction instruction) {
            Label = label;
            Instruction = instruction;
        }

        /// <summary>
        ///     Construct just from an instruction.
        /// </summary>
        public TokenizedSourceLine([NotNull] SourceInstruction instruction)
            : this(null, instruction) {}

        // Properties /////////////////////////////////////////////////////////

        [CanBeNull] public string Label { get; }
        [CanBeNull] public SourceInstruction Instruction { get; }

        public bool HasLabel       => Label != null;
        public bool HasInstruction => Instruction != null;
        public bool IsEmpty        => !HasLabel && !HasInstruction;

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