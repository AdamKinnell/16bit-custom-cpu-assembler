using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Assembler.Constants;
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

        /// <summary> Maps operand types to a regex that matches them. </summary>
        private static readonly Dictionary<OperandParts, string> REGEX_MAPPINGS
            = new Dictionary<OperandParts, string> {
                {OperandParts.Register, RegexDefinitions.RegisterOperand},
                {OperandParts.Immediate, RegexDefinitions.ImmediateOperand},
                {OperandParts.BaseOffset, RegexDefinitions.BaseOffsetOperand},
            };

        /// <summary>
        ///     Compile a regex to match the given combination of operands.
        /// </summary>
        [NotNull]
        public Regex CompileRegexFor([NotNull] OperandParts[] parts) {
            var regex = String.Join(" ", parts.Select(part => REGEX_MAPPINGS[part]));
            return new Regex('^' + regex + '$', RegexOptions.Compiled);
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
    ///     Represents a single register operand.
    /// </summary>
    public class OperandFormat1R : IOperandFormat {

        private static readonly Regex REGEX = new OperandFormatRegexHelper().CompileRegexFor(
            new[] {OperandFormatRegexHelper.OperandParts.Register});

        /// <inheritdoc />
        public bool IsOfFormat(string operands) => REGEX.IsMatch(operands);

        /// <inheritdoc />
        public IOperandList CreateOperandList(string operands) {
            //return new OperandList1R();
        }
    }
}