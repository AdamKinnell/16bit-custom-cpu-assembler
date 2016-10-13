using System.Collections.Generic;
using Assembler.Instructions.Operands.OperandTypes;
using JetBrains.Annotations;

namespace Assembler.Instructions.Operands {

    /// <summary>
    ///     Represents a list of zero or more operands.
    /// </summary>
    public class OperandList {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from a list of operands.
        /// </summary>
        /// <param name="operands"> Order matters. </param>
        public OperandList([NotNull, ItemNotNull] IReadOnlyCollection<IOperand> operands) {
            Operands = operands;
            Format = new OperandFormat(operands);
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary> Get the operands in the list. </summary>
        [NotNull, ItemNotNull] public IReadOnlyCollection<IOperand> Operands { get; }

        /// <summary> Get the format of the operands. </summary>
        [NotNull] public OperandFormat Format { get; }
    }
}