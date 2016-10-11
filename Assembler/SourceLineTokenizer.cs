using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Assembler {
    public class SourceLineTokenizer {

        private string[] TokenizeOperands([NotNull] string operands) {

            // Determine operand format.

            // Free Type
            // 1R Type
            // 1I Type
            // 2R Type
            // 2R-1I Type
            // Offset Type
            // 1R-Offset Type
            return null;
        }

        /// <summary>
        ///     Tokenize a single line of source code.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string Tokenize([NotNull] string line) {

            //var match = new Regex("").Match(CleanupLine(line));

            //// Split line.
            //var match = new Regex(@"^([a-zA-Z])(.*)").Match(CleanupLine(line));
            //if (!match.Success)
            //    return null;

            //// Tokenize mnemonic.
            //var inst = new LogicalInstruction();
            //inst.Mnemonic = match.Groups[1].Value;
            //if ((inst.Operands = TokenizeOperands(operands)) == null)
            //    return null; // Invalid operand format.

            //return inst;

            return null;
        }
    }
}