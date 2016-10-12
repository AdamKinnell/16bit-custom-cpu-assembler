using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Assembler.Splitter {

    /// <summary>
    ///     Splits a single line in a source file into it's constituent parts.
    /// </summary>
    public class SourceLineSplitter {

        // Constants //////////////////////////////////////////////////////////

        private static readonly Regex SPLIT_REGEX = new Regex(
            // Start
            @"^" +
            // Label
            @"(?:([a-zA-Z][_a-zA-Z0-9]+?) ?:)? ?" +
            // Instruction (Mnemonic + Operands)
            @"(?:([a-zA-Z]+?)( .*?)?)??" +
            // Comment
            @" ?(?:#(.*))?" +
            // End
            @"$", RegexOptions.Compiled);

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
        ///     Splits the given source line into strings,
        ///     each representing a different part.
        /// 
        ///     The output of this function may be passed to a tokenizer
        ///     for further processing.
        /// </summary>
        /// <returns> Null if the given line is malformed and cannot be split. </returns>
        [CanBeNull]
        public SourceLine SplitLine([NotNull] string line) {
            line = CleanupLine(line);

            if (String.IsNullOrWhiteSpace(line))
                return SourceLine.CreateEmpty();

            var match = SPLIT_REGEX.Match(line);
            if (match.Success) {
                return new SourceLine(
                    label:    match.Groups[1].Success ? match.Groups[1].Value.Trim() : null,
                    mnemonic: match.Groups[2].Success ? match.Groups[2].Value.Trim() : null,
                    operands: match.Groups[3].Success ? match.Groups[3].Value.Trim() : null,
                    comment:  match.Groups[4].Success ? match.Groups[4].Value.Trim() : null
                );
            } else {
                return null;
            }
        }
    }
}