using JetBrains.Annotations;

namespace Assembler.Instructions.Operands {

    /// <summary>
    ///     Represents the possible formats of instruction operands.
    /// </summary>
    public enum OperandFormatType {
        FormatFree, //    <MNEM>
        Format1R, //      <MNEM> $A
        Format2R, //      <MNEM> $A $B
        Format1I, //      <MNEM> IMM
        Format1R1I, //    <MNEM> $A IMM
        Format2R2I, //    <MNEM> $A $B IMM
        FormatOffset, //  <MNEM> IMM($B)
        Format1ROffset // <MNEM> $A IMM($B)
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
        bool IsOfFormat(string operands);

        /// <summary>
        /// 
        /// </summary>
        /// <returns> </returns>
        IOperandList CreateOperandList();
    }

    /// <summary>
    /// Represents an empty set of operands.
    /// </summary>
    public class OperandFormatFree : IOperandFormat {

        /// <inheritdoc />
        public bool IsOfFormat([NotNull] string operands) => operands == "";

        /// <inheritdoc />
        public IOperandList CreateOperandList() {
            throw new System.NotImplementedException();
        }
    }

}