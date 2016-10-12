using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Assembler.Instructions.Operands {

    /// <summary>
    ///     All possible formats of instruction operands.
    /// </summary>
    public enum OperandFormatType {
        FormatEmpty, //   <MNEM>
        Format1R, //      <MNEM> $A
        Format2R, //      <MNEM> $A $B
        Format1I, //      <MNEM> IMM
        Format1R1I, //    <MNEM> $A IMM
        Format2R1I, //    <MNEM> $A $B IMM
        FormatOffset, //  <MNEM> IMM($B)
        Format1ROffset // <MNEM> $A IMM($B)
    }

    /// <summary>
    ///     Creates regular expressions to match an operand list.
    /// </summary>
    class OperandFormatRegexHelper {

        /// <summary> Represents the parts of an operand list. </summary>
        public enum OperandParts {
            Register,
            Immediate,
            BaseOffset
        }

        private static readonly Dictionary<OperandParts, string> REGEX_MAPPINGS
            = new Dictionary<OperandParts, string> {
                {OperandParts.Register, @"(\$[a-zA-Z0-9]+)"},
                {OperandParts.Immediate, @"((?:0x|0b|)[0-9a-fA-F]+)"},
                {OperandParts.BaseOffset, @"c"},
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parts"> </param>
        /// <returns> </returns>
        [NotNull]
        public Regex CompileRegexFor([NotNull] OperandParts[] parts) {
            var regex = String.Join(" ", parts.Select(part => REGEX_MAPPINGS[part]));
            return new Regex( '^' + regex + '$', RegexOptions.Compiled);
        }
    }

    /// <summary>
    ///     Represents a format of instruction operands.
    /// </summary>
    public interface IOperandFormat {

        /// <summary>
        ///     Check if the given string of operands
        ///     is of this operand format, and an operand list
        ///     of this type can be created.
        /// </summary>
        /// <param name="operands"> </param>
        /// <returns> </returns>
        bool IsOfFormat([NotNull] string operands);

        /// <summary>
        ///     Cre
        /// </summary>
        /// <returns> </returns>
        [NotNull]
        IOperandList CreateOperandList([NotNull] string operands);
    }

    /// <summary>
    ///     Represents an empty set of operands.
    /// </summary>
    public class OperandFormatEmpty : IOperandFormat {

        /// <inheritdoc />
        public bool IsOfFormat(string operands) => operands == "";

        /// <inheritdoc />
        public IOperandList CreateOperandList(string operands) => new OperandListEmpty();
    }

    /// <summary>
    /// 
    /// </summary>
    public class OperandFormat1R : IOperandFormat {

        private static readonly Regex regex
            = new Regex(@"\$", RegexOptions.Compiled);

        /// <inheritdoc />
        public bool IsOfFormat(string operands) => regex.IsMatch(operands);

        /// <inheritdoc />
        public IOperandList CreateOperandList(string operands) {
            throw new NotImplementedException();
        }
    }
}