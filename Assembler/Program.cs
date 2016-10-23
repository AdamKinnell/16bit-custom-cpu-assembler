using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assembler.Instructions;
using Assembler.Lexer.Tokenizer;
using JetBrains.Annotations;

namespace Assembler {
    class Program {

        // Static Fields //////////////////////////////////////////////////////

        private const string RESOURCE_FOLDER = @"../../Resources/";

        private static readonly string SOURCE_PATH = Path.Combine(RESOURCE_FOLDER, "source.asm");
        private static readonly string TEXT_PATH = Path.Combine(RESOURCE_FOLDER, "text.txt");
        private static readonly string DATA_PATH = Path.Combine(RESOURCE_FOLDER, "data.txt");

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
        ///     Tokenize all the lines in the file at the specified path.
        /// </summary>
        /// <exception cref="ArgumentException"> If tokenizing fails. </exception>
        [NotNull, ItemNotNull]
        private static IEnumerable<TokenizedSourceLine> TokenizeLinesFromFile([NotNull] string path) {
            using (var source_file = File.OpenText(path)) {
                return TokenizeLines(source_file);
            }
        }

        /// <summary>
        ///     Assemble the given instruction into machine code.
        /// </summary>
        /// <exception cref="ArgumentException"> If the instruction is invalid. </exception>
        private static Int32 AssembleSourceInstruction([NotNull] SourceInstruction instruction) {
            var registry = Constants.Instructions.GetRegistry();
            var native = registry.Find(instruction.Format);

            if (native == null)
                throw new ArgumentException("No instruction of the given format has been registered.");
            else
                return native.AssembleWithOperands(instruction.Operands);
        }

        // Entry Point ////////////////////////////////////////////////////////

        private static void Main([NotNull] string[] args) {

            // Tokenize lines from source file.
            var lines = TokenizeLinesFromFile(SOURCE_PATH);

            // Assemble instructions.
            var machine_code = lines
                .Where(x => x.HasInstruction)
                .Select(x => AssembleSourceInstruction(x.Instruction));

            // Resolve label addresses.
            // TODO

            // Write machine code.
            using (var writer = new StreamWriter(TEXT_PATH)) {
                writer.Write("v2.0 raw\r\n");
                foreach (Int32 word in machine_code) {
                    var bytes = BitConverter.GetBytes(word);
                    if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

                    foreach (byte b in bytes)
                        writer.Write($@"{b:X2}");
                    writer.Write(' ');
                }
                writer.WriteLine();
            }

            // Done!
            Console.WriteLine("Goodbye World!");
            Console.ReadKey();
        }
    }
}