using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace Assembler.Lexer.Tokenizer {

    /// <summary>
    ///     Tokenizes every line in a stream.
    /// </summary>
    public class SourceStreamTokenizer {

        /// <summary>
        ///     Read line-by-line and tokenize each one.
        ///     Only lines with labels or instructions are returned.
        /// </summary>
        /// <exception cref="ArgumentException"> If tokenizing fails. </exception>
        [NotNull, ItemNotNull]
        public IEnumerable<TokenizedSourceLine> TokenizeLines([NotNull] TextReader input) {
            var lines = new List<TokenizedSourceLine>();
            while (true) {
                string line = input.ReadLine();
                if (line != null) {
                    var tokenized = new SourceLineTokenizer().TokenizeLine(line);
                    if (tokenized.HasLabel || tokenized.HasInstruction)
                        lines.Add(tokenized);
                } else {
                    return lines;
                }
            }
        }

        /// <summary>
        ///     Read line-by-line and tokenize each one.
        ///     Only lines with labels or instructions are returned.
        /// </summary>
        /// <exception cref="ArgumentException"> If tokenizing fails. </exception>
        [NotNull]
        public IEnumerable<TokenizedSourceLine> TokenizeLines([NotNull] Stream stream)
            => TokenizeLines(new StreamReader(stream));
    }
}