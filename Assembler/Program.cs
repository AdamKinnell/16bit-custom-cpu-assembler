using System;
using System.Collections.Generic;
using System.IO;
using Assembler.Lexer.Tokenizer;
using JetBrains.Annotations;

namespace Assembler {
    class Program {

        // Static Fields //////////////////////////////////////////////////////

        private const string RESOURCE_FOLDER = @"../../Resources/";

        private static readonly string SOURCE_PATH = Path.Combine(RESOURCE_FOLDER, "source.asm");
        private static readonly string TEXT_PATH = Path.Combine(RESOURCE_FOLDER, "text.bin");
        private static readonly string DATA_PATH = Path.Combine(RESOURCE_FOLDER, "data.bin");

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Read line-by-line and tokenize each one.
        ///     Only lines with labels or instructions are returned.
        /// </summary>
        /// <exception cref="ArgumentException"> If tokenizing fails. </exception>
        [NotNull, ItemNotNull]
        private static IEnumerable<TokenizedSourceLine> TokenizeLines([NotNull] TextReader input) {
            var lines = new List<TokenizedSourceLine>();
            while (true) {
                var line = input.ReadLine();
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
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"> If tokenizing fails. </exception>
        [NotNull, ItemNotNull]
        private static IEnumerable<TokenizedSourceLine> TokenizeLinesFromFile([NotNull] string path) {
            using (var source_file = File.OpenText(path)) {
                return TokenizeLines(source_file);
            }
        }

        // Entry Point ////////////////////////////////////////////////////////

        private static void Main([NotNull] string[] args) {

            // Tokenize lines from source file.
            var lines = TokenizeLinesFromFile(SOURCE_PATH);

            foreach (var line in lines) {
                if (!line.HasInstruction) continue;

                var registry = Constants.Instructions.GetRegistry();
                var instruction = registry.Find(line.Instruction);
            }

            // Assemble instructions.
            // TODO

            // Resolve label addresses.
            // TODO

            // Write machine code.
            // TODO

            Console.WriteLine("Goodbye World!");
            Console.ReadKey();
        }
    }
}