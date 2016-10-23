using System;
using System.Collections.Generic;
using System.Linq;
using Assembler.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Operands {

    /// <summary>
    ///     Represents a list of zero or more operands.
    /// </summary>
    public class OperandList {

        // Static Functions ///////////////////////////////////////////////////

        [NotNull]
        public static OperandList CreateEmpty()
            => new OperandList();

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from a list of operands.
        /// </summary>
        /// <param name="operands"> Order matters. </param>
        public OperandList([NotNull, ItemNotNull] IReadOnlyCollection<IOperand> operands) {
            Operands = operands;
            Format = new OperandFormat(operands);
        }

        /// <summary>
        ///     Construct from a variable number of arguments.
        /// </summary>
        /// <param name="operands"> Order matters. </param>
        public OperandList([NotNull] params IOperand[] operands)
            : this((IReadOnlyCollection<IOperand>) operands) {}

        // Properties /////////////////////////////////////////////////////////

        /// <summary> Get the operands in the list. </summary>
        [NotNull, ItemNotNull] public IReadOnlyCollection<IOperand> Operands { get; }

        /// <summary> Get the format of the operands. </summary>
        [NotNull] public OperandFormat Format { get; }

        // Operator Overloads /////////////////////////////////////////////////

        /// <summary>
        ///     Get the i'th operand in the list.
        /// </summary>
        /// <param name="index"> Starts from 0. </param>
        [CanBeNull] public IOperand this[int index]
            => Operands.ElementAt(index);

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is OperandList && Equals((OperandList) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] OperandList other) {
            if (Operands.Count != other.Operands.Count) return false;
            if (Operands.Count == 0) return true;
            return Operands.Zip(other.Operands, (x, y) => x.Equals(y))
                           .Aggregate((x, y) => x && y);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}