using JetBrains.Annotations;

namespace Assembler.Constants {

    /// <summary>
    ///     Contains a number of regular expression strings for matching
    ///     various parts of a source file.
    /// </summary>
    class RegexDefinitions {

        /// <summary>
        ///     Make the given regex string optional.
        /// </summary>
        [NotNull]
        public static string MakeOptional([NotNull] string regex)
            => "(?:" + regex + ")?";

        /// <summary>
        /// Make a named capture group for the givrn regex string.
        /// </summary>
        [NotNull]
        public static string MakeCapture([NotNull] string regex, [NotNull] string name)
            => @"(?<" + name + '>' + regex + @")";

        // Line ///////////////////////////////////////////////////////////////

        // Named capture group: "label"
        // Named capture group: "mnemonic"
        // Named capture group: "operands"
        // 1 named capture group per operand: "operand"
        // Named capture group: "comment"
        [NotNull] public static string SourceLine
            => '^'
            + MakeOptional(LabelDefinition + @" *")
            + MakeOptional(Instruction)
            + MakeOptional(@" *" + Comment)
            + '$';

        // Label //////////////////////////////////////////////////////////////

        // No capture groups.
        [NotNull] public static string LabelName
            => @"[_a-zA-Z][_a-zA-Z0-9]+?";

        // Named capture group: "label"
        [NotNull] public static string LabelDefinition
            => MakeCapture(LabelName, "label") + @" *:";

        // Instruction ////////////////////////////////////////////////////////

        // Named capture group: "mnemonic"
        // Named capture group: "operands"
        // 1 named capture group per operand: "operand"
        [NotNull] public static string Instruction
            => Mnemonic + MakeOptional(' ' + Operands);

        // Named capture group: "mnemonic"
        [NotNull] public static string Mnemonic
            => MakeCapture(@"[a-zA-Z]+", "mnemonic");

        // Named capture group: "operands"
        // 1 named capture group per operand: "operand"
        [NotNull] public static string Operands {
            get {
                string multiple_operands = $@"(?:{SingleOperand} )*{SingleOperand}";
                return MakeCapture(multiple_operands, "operands");
            }
        }

        // Named capture group: "operand"
        [NotNull] public static string SingleOperand
            => MakeCapture($@"(?:{BaseOffsetOperand}|{RegisterOperand}|{ImmediateOperand})", "operand");

        // No capture groups.
        [NotNull] public static string RegisterOperand
            => @"\$[a-zA-Z0-9]+";

        // No capture groups.
        [NotNull] public static string ImmediateOperand
            => $@"(?:{IntegerImmediate}|{LabelName})";

        // No capture groups.
        [NotNull] public static string IntegerImmediate
            => $@"(?:{DecimalImmediate}|{HexImmediate}|{BinaryImmediate})";

        // No capture groups.
        [NotNull] public static string DecimalImmediate
            => @"[0-9]+";

        // No capture groups.
        [NotNull] public static string HexImmediate
            => @"0x[0-9a-fA-F]+";

        // No capture groups.
        [NotNull] public static string BinaryImmediate
            => @"0b[01]+";

        // No capture groups.
        [NotNull] public static string BaseOffsetOperand
            => $@"{ImmediateOperand} *\( *{RegisterOperand} *\)";

        // Comment ////////////////////////////////////////////////////////////

        // Named capture group: "comment"
        [NotNull] public static string Comment
            => @"#" + MakeCapture(@".*", "comment");
    }
}