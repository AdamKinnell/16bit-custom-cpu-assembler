using System;
using System.IO;
using JetBrains.Annotations;

namespace Assembler {
    class Program {
        // ReSharper disable once InconsistentNaming
        private static void Main([NotNull] string[] args) {

            const string SOURCE_PATH = "../../source.asm";

            try {
                using (var source = new StreamReader(SOURCE_PATH)) {

                    // Tokenize lines.
                    // TODO

                    // Tokenize instructions.
                    // TODO

                    // Resolve label addresses.
                    // TODO

                    // Assemble instructions.
                    // TODO
                }
            } catch (Exception) {
                Console.WriteLine("Unable to open file.");
            }

            Console.WriteLine("Goodbye World!");
            Console.ReadKey();
        }
    }
}
