using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Assembler.Constants;
using Assembler.Instructions;
using Assembler.Label;
using Assembler.Operands;
using Assembler.Operands.Parsers;
using JetBrains.Annotations;

namespace Assembler.Lexer.Tokenizer {

    /// <summary>
    ///     Lexes a single source line and splits it into tokens.
    /// 
    ///     From Wikipedia:
    ///     Lexing itself can be divided into two stages:
    ///     - Scanning, which segments the input sequence into groups and categorizes these into token classes.
    ///     - Evaluating, which converts the raw input characters into a processed value.
    /// </summary>
    public class SourceLineTokenizer {

        // Constants //////////////////////////////////////////////////////////

        private static readonly Regex SPLIT_REGEX = new Regex(RegexDefinitions.SourceLine, RegexOptions.Compiled);

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Remove unnecessary characters from the source line.
        /// </summary>
        [NotNull]
        private string CleanupLine([NotNull] string line) {
            // Remove unnecessary punctuation.
            line = line.Replace(",", " ");

            // Remove extra whitespace.
            line = new Regex(@"\s+").Replace(line, " ");
            line = line.Trim();

            return line;
        }

        /// <summary>
        ///     Tokenize operands from a match group.
        /// </summary>
        [NotNull, SuppressMessage("ReSharper", "PossibleNullReferenceException"), SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        private OperandList TokenizeOperands([CanBeNull] Group match_group) {
            if ((match_group == null) || !match_group.Success)
                return OperandList.CreateEmpty();

            var operands = match_group
                .Captures.Cast<Capture>()
                .Select(cap => cap.Value) // Get string token.
                .Select(val => val.Replace(" ", "")) // Remove all spaces.
                .Select(val => new OperandParser().Parse(val)) // Parse as token.
                .ToArray();

            return new OperandList(operands);
        }

        /// <summary>
        ///     Get the trimmed value of a match group, if the match was successful.
        /// </summary>
        /// <param name="match"> The match object that contains the groups. </param>
        /// <param name="group"> The name of the group. </param>
        /// <returns> Null if the group was not matched. </returns>
        [CanBeNull]
        private string GetMatchGroupValue([NotNull] Match match, [NotNull] string group)
            => match.Groups[group].Success ? match.Groups[group].Value.Trim() : null;

        /// <summary>
        ///     Create a source line from the tokens in the match.
        /// </summary>
        [NotNull]
        private TokenizedSourceLine CreateSourceLine([NotNull] Match match) {
            var label_name = GetMatchGroupValue(match, "label");
            var label = label_name == null ? null
                : new LabelDefinition(label_name);

            var mnemonic = GetMatchGroupValue(match, "mnemonic");
            var operands = TokenizeOperands(match.Groups["operand"]);
            var instruction = mnemonic == null ? null
                : new SourceInstruction(mnemonic, operands);

            return new TokenizedSourceLine(label, instruction);
        }

        /// <summary>
        ///     Splits the given source line into tokens.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If the given line is malformed and cannot be tokenized.
        /// </exception>
        [NotNull]
        public TokenizedSourceLine TokenizeLine([NotNull] string line) {
            line = CleanupLine(line);

            if (String.IsNullOrWhiteSpace(line))
                return TokenizedSourceLine.CreateEmpty();

            var match = SPLIT_REGEX.Match(line);
            if (match.Success) {
                return CreateSourceLine(match);
            } else {
                throw new ArgumentException("Line is malformed: " + line, nameof(line));
            }
        }
    }
}