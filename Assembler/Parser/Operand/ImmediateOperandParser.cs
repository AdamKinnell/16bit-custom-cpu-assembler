using System;
using System.Text.RegularExpressions;
using Assembler.Constants;
using Assembler.Instructions.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Parser.Operand {

    /// <summary>
    ///     Parses a numerical immediate operand as specified in the source code.
    /// </summary>
    public class ImmediateOperandParser {

        // Static Fields //////////////////////////////////////////////////////

        private static readonly Regex REGEX_DECIMAL
            = new Regex(RegexDefinitions.WholeInput(RegexDefinitions.DecimalImmediate),
                        RegexOptions.Compiled);

        private static readonly Regex REGEX_HEX
            = new Regex(RegexDefinitions.WholeInput(RegexDefinitions.HexImmediate),
                        RegexOptions.Compiled);

        private static readonly Regex REGEX_BINARY
            = new Regex(RegexDefinitions.WholeInput(RegexDefinitions.BinaryImmediate),
                        RegexOptions.Compiled);

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Attempt to parse the given immediate as a label name.
        /// </summary>
        /// <returns> Null if parsing failed. </returns>
        [CanBeNull]
        private ImmediateOperand TryParseAsLabel([NotNull] string immediate) {
            // TODO: Implement labels.
            return null;
        }

        /// <summary>
        ///     Attempt to parse the given immediate as a decimal integer.
        /// </summary>
        /// <returns> Null if parsing failed. </returns>
        [CanBeNull]
        private ImmediateOperand TryParseAsDecimal([NotNull] string immediate) {
            if (REGEX_DECIMAL.Match(immediate).Success) {
                var value = Convert.ToInt32(immediate, 10);
                return new ImmediateOperand(value);
            } else {
                return null;
            }
        }

        /// <summary>
        ///     Attempt to parse the given immediate as a hexadecimal value.
        /// </summary>
        /// <returns> Null if parsing failed. </returns>
        [CanBeNull]
        private ImmediateOperand TryParseAsHex([NotNull] string immediate) {
            if (REGEX_HEX.Match(immediate).Success) {
                var value = Convert.ToInt32(immediate, 16);
                return new ImmediateOperand(value);
            } else {
                return null;
            }
        }

        /// <summary>
        ///     Attempt to parse the given immediate as a binary value.
        /// </summary>
        /// <returns> Null if parsing failed. </returns>
        [CanBeNull]
        private ImmediateOperand TryParseAsBinary([NotNull] string immediate) {
            if (REGEX_BINARY.Match(immediate).Success) {
                immediate = immediate.Substring(2); // Remove "0b" prefix.
                var value = Convert.ToInt32(immediate, 2);
                return new ImmediateOperand(value);
            } else {
                return null;
            }
        }

        /// <summary>
        ///     Parse the given immediate value string as an operand.
        /// </summary>
        /// <exception cref="ArgumentException"> If no valid immediate specified. </exception>
        [NotNull]
        public ImmediateOperand Parse([NotNull] string immediate) {
            var operand =
                TryParseAsLabel(immediate) ??
                TryParseAsDecimal(immediate) ??
                TryParseAsHex(immediate) ??
                TryParseAsBinary(immediate);

            if (operand == null) {
                throw new ArgumentException("Could not evaluate as immediate.",
                                            nameof(immediate));
            } else {
                return operand;
            }
        }
    }
}