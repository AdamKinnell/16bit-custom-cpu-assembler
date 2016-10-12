using Assembler.Splitter;
using JetBrains.Annotations;

namespace Assembler.Tokenizer {
    public class SourceLineTokenizer {

        /// <summary>
        ///     Tokenize a single line of source code.
        /// </summary>
        /// <returns>
        ///     Null if the given line was not able to be tokenized.
        ///     i.e. the general syntax is incorrect.
        /// </returns>
        [CanBeNull]
        public string Tokenize([NotNull] string line) {

            var split_line = new SourceLineSplitter().SplitLine(line);
            if (split_line == null) return null; // General invalid format.

            return null;
        }
    }
}