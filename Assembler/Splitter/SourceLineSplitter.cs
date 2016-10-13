using System;
using System.Text.RegularExpressions;
using Assembler.Constants;
using JetBrains.Annotations;

namespace Assembler.Splitter {

    /// <summary>
    ///     Splits a single line in a source file into it's constituent parts.
    /// </summary>
    public class SourceLineSplitter {

        // Constants //////////////////////////////////////////////////////////

        private static readonly Regex SPLIT_REGEX = new Regex(RegexDefinitions.SourceLine);

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
        ///     Create a source line from the tokens in the match.
        /// </summary>
        [NotNull]
        private SourceLine CreateSourceLine([NotNull] Match match) {
            var label = match.Groups["label"].Success ? match.Groups["label"].Value.Trim() : null;
            var mnemonic = match.Groups["mnemonic"].Success ? match.Groups["mnemonic"].Value.Trim() : null;
            var operands = match.Groups["operands"].Success ? match.Groups["operands"].Value.Trim() : null;
            var comment = match.Groups["comment"].Success ? match.Groups["comment"].Value.Trim() : null;

            return new SourceLine(label, mnemonic, operands, comment);
        }

        /// <summary>
        ///     Splits the given source line into strings,
        ///     each representing a different part.
        /// 
        ///     The output of this function may be passed to a tokenizer
        ///     for further processing.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If the given line is malformed and cannot be split.
        /// </exception>
        [NotNull]
        public SourceLine SplitLine([NotNull] string line) {
            line = CleanupLine(line);

            if (String.IsNullOrWhiteSpace(line))
                return SourceLine.CreateEmpty();

            var match = SPLIT_REGEX.Match(line);
            if (match.Success) {
                return CreateSourceLine(match);
            } else {
                throw new ArgumentException("Line is malformed.", nameof(line));
            }
        }
    }
}