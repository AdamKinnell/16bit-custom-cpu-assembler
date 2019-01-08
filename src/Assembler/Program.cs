using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assembler.Instructions;
using Assembler.Lexer.Tokenizer;
using JetBrains.Annotations;

namespace Assembler {

    class Program {

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Tokenize all the lines in the file at the specified path.
        /// </summary>
        /// <exception cref="ArgumentException"> If tokenizing fails. </exception>
        [NotNull, ItemNotNull]
        private static IEnumerable<TokenizedSourceLine> TokenizeLinesFromFile([NotNull] string path) {
            using (var source_file = File.OpenText(path)) {
                return new SourceStreamTokenizer().TokenizeLines(source_file);
            }
        }

        /// <summary>
        ///     Assemble the given instruction into machine code.
        /// </summary>
        /// <returns> Null if an error occurs. </returns>
        [NotNull]
        private static MachineCode AssembleSourceInstruction([NotNull] SourceInstruction instruction) {
            var registry = Constants.Instructions.GetRegistry();
            var native = registry.TryFind(instruction.Format);

            if (native == null)
                throw new ArgumentException("Unknown Instruction: " + instruction.Format);

            return native.AssembleWithOperands(instruction.Operands);
        }

        // Menu Options ///////////////////////////////////////////////////////

        /// <summary>
        ///     Print out usage information.
        /// </summary>
        private static void MenuPrintHelp() => Console.WriteLine(
            "16-Bit Custom CPU Assembler 0.1.0\n" +
            "By Adam Kinnell, 2016\n" +
            "\n" +
            "Program Usage:\n" +
            "  assemble <source_file_in> <binary_file_out>\n" +
            "    - Assemble the source code in <source_file_in> and write the machine code to <binary_file_out>\n" +
            "  assemble <source_file_in>\n" +
            "    - Assemble the source code in <source_file_in> and write the machine code to './out'\n" +
            "  assemble\n" +
            "    - Assemble the source code in './in' and write the machine code to './out'\n" +
            "  instructions\n" +
            "    - Print a list of supported instructions.\n" +
            "  help\n" +
            "    - Show this information.\n" +
            "\n" +
            "Unsupported Features:\n" +
            "   - Labels\n" +
            "   - Character immediates\n" +
            "   - Assembler directives\n" +
            "   - Macros\n" +
            "   - Defining data in memory\n"
        );

        /// <summary>
        ///     Print out a list of all instructions that are supported.
        /// </summary>
        private static void MenuPrintSupportedInstructions() {
            var registry = Constants.Instructions.GetRegistry();
            var instructions = registry.GetRegisteredInstructions();

            var strings = instructions
                .Select(x => x.Format.ToString())
                .OrderBy(x => x);

            Console.WriteLine("Supported Instructions:\n");
            foreach (string s in strings) Console.WriteLine(s);
        }

        /// <summary>
        ///     Assemble the source code in {file_in} and write the machine code to {file_out}.
        /// </summary>
        private static void MenuAssembleSourceCode([NotNull] string file_in, [NotNull] string file_out) {

            Console.WriteLine($"Source code in file:  '{file_in}'");
            Console.WriteLine($"Machine code in file: '{file_out}'");

            // Tokenize lines from source file.
            var lines = TokenizeLinesFromFile(file_in);

            // Assemble instructions.
            var machine_code = lines
                .Where(x => x.HasInstruction)
                .Select(x => AssembleSourceInstruction(x.Instruction));

            // Resolve label addresses.
            // TODO

            // Write machine code.
            using (var writer = new StreamWriter(file_out)) {
                writer.Write("v2.0 raw\r\n");
                foreach (var instruction in machine_code) {
                    foreach (byte b in instruction.AsBigEndianBytes())
                        writer.Write($@"{b:X2}");
                    writer.Write(' ');
                }
                writer.WriteLine();
            }

            // Done!
            Console.WriteLine("File successfully assembled.");
        }

        // Entry Point ////////////////////////////////////////////////////////

        private static int RunProgram([NotNull] IReadOnlyList<string> args) {

            // Must provide arguments.
            if (args.Count == 0) {
                MenuPrintHelp();
                return 1;
            }

            // Parse arguments.
            int num_args = args.Count - 1;
            string command = args[0].ToLower();

            // Determine command.
            if ((command == "assemble") && (num_args >= 0) && (num_args <= 2)) {
                string source = (num_args >= 1) && !String.IsNullOrWhiteSpace(args[1]) ? args[1] : "in";
                string dest   = (num_args >= 2) && !String.IsNullOrWhiteSpace(args[2]) ? args[2] : "out";

                MenuAssembleSourceCode(source, dest);
                return 0; // If no exception occurs.

            } else if ((command == "instructions") && (num_args == 0)) {
                MenuPrintSupportedInstructions();
                return 0;

            } else if ((command == "help") && (num_args == 0)) {
                MenuPrintHelp();
                return 1;

            } else {
                Console.WriteLine("\n-------- Bad command line format --------\n");
                MenuPrintHelp();
                return 1;
            }

        }

        private static int Main([NotNull] string[] args) {
            int return_code;

            try {
                return_code = RunProgram(args);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return_code = 1;
            }

            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
            return return_code;
        }
    }
}