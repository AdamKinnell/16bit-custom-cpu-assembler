using System;
using System.Linq;
using System.Text.RegularExpressions;
using Assembler.Constants;
using JetBrains.Annotations;

namespace Assembler.Lexer.Scanner {

    /// <summary>
    ///     Scans a single source line and splits it into token strings for later evaluation.
    /// 
    ///     From Wikipedia:
    ///     Lexing itself can be divided into two stages:
    ///     -> Scanning, which segments the input sequence into groups and categorizes these into token classes.
    ///     - Evaluating, which converts the raw input characters into a processed value.
    /// </summary>
    public class SourceLineScanner {

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
        /// <param name="match_group"> </param>
        [CanBeNull]
        private string[] TokenizeOperands([CanBeNull] Group match_group) {
            if ((match_group == null) || !match_group.Success) return null;
            return match_group.Captures.Cast<Capture>()
                              .Select(cap => cap?.Value.Replace(" ", "")) // Remove all spaces.
                              .ToArray();
        }

        /// <summary>
        ///     Create a source line from the tokens in the match.
        /// </summary>
        [NotNull]
        private ScannedSourceLine CreateSourceLine([NotNull] Match match) {
            var label    = match.Groups["label"].Success    ? match.Groups["label"].Value.Trim()    : null;
            var mnemonic = match.Groups["mnemonic"].Success ? match.Groups["mnemonic"].Value.Trim() : null;
            var comment  = match.Groups["comment"].Success  ? match.Groups["comment"].Value.Trim()  : null;
            var operands = TokenizeOperands(match.Groups["operand"]);
            return new ScannedSourceLine(label, mnemonic, operands, comment);
        }

        /// <summary>
        ///     Splits the given source line into strings,
        ///     each representing a different token of the input.
        /// 
        ///     The output of this function may be passed to a parser
        ///     for further processing.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If the given line is malformed and cannot be tokenized.
        /// </exception>
        [NotNull]
        public ScannedSourceLine TokenizeLine([NotNull] string line) {
            line = CleanupLine(line);

            if (String.IsNullOrWhiteSpace(line))
                return ScannedSourceLine.CreateEmpty();

            var match = SPLIT_REGEX.Match(line);
            if (match.Success) {
                return CreateSourceLine(match);
            } else {
                throw new ArgumentException("Line is malformed.", nameof(line));
            }
        }
    }
}