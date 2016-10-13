using System;

namespace Assembler.Instructions.Operands.OperandTypes {

    /// <summary>
    ///     Represents an operand specifying an immediate compile-time value.
    /// </summary>
    public class ImmediateOperand : IOperand {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct a new immediate operand from it's value.
        /// </summary>
        /// <param name="value"> No range limitations. </param>
        public ImmediateOperand(int value) {
            Value = value;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The lower 16 bits specify the immediate value.
        /// </summary>
        public Int32 Value { get; }

    }
}