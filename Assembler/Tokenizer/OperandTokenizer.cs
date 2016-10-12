using Assembler.Instructions.Operands;
using JetBrains.Annotations;

namespace Assembler.Tokenizer {

    /// <summary>
    ///     Converts a string of instruction operands to a tokenized form.
    /// </summary>
    class OperandTokenizer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operands"></param>
        /// <returns></returns>
        [CanBeNull]
        public IOperandList Tokenizer([NotNull] string operands) {

            /*
            for each possible format:
                see if it matches
                if so, create it.
            */

            // Determine operand format.
            //if (OperandFormatFree.TryCreate(operands))

            // Free Type
            // 1R Type
            // 1I Type
            // 2R Type
            // 2R-1I Type
            // Offset Type
            // 1R-Offset Type
            return null;
        }
    }
}