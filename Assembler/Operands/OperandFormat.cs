using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Assembler.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Operands {

    /// <summary>
    ///     Represents the expected or given format of a list of operands.
    /// </summary>
    public class OperandFormat {

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from a list of operands.
        /// </summary>
        public OperandFormat([NotNull, ItemNotNull] IEnumerable<IOperand> operands) {
            OperandTypes = operands.Select(operand => operand.GetType()).ToList();
        }

        /// <summary>
        ///     Construct directly from a list of types.
        /// </summary>
        /// <param name="operand_types"> Must implement interface IOperand. </param>
        /// <exception cref="ArgumentException"> If any type does not implement IOperand. </exception>
        [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        public OperandFormat([NotNull, ItemNotNull] IReadOnlyCollection<Type> operand_types) {
            if (operand_types.Any(operand_type => !operand_type.GetInterfaces().Contains(typeof(IOperand)))) {
                throw new ArgumentException("Not a type of operand.", nameof(operand_types));
            }
            OperandTypes = operand_types;
        }

        /// <summary>
        ///     Construct from a variable number of type arguments.
        /// </summary>
        /// <param name="operand_types"> Must be subclasses of IOperand. </param>
        // ReSharper disable once NotNullMemberIsNotInitialized
        public OperandFormat([NotNull] params Type[] operand_types)
            : this((IReadOnlyCollection<Type>) operand_types) {}

        // Properties /////////////////////////////////////////////////////////

        [NotNull, ItemNotNull] public IReadOnlyCollection<Type> OperandTypes { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override string ToString()
            => String.Join(
                ", ",
                OperandTypes.Select(
                    type => {
                        if (type == typeof(RegisterOperand))
                            return "$REG";
                        else if (type == typeof(ImmediateOperand))
                            return "IMM";
                        else if (type == typeof(BaseOffsetOperand))
                            return "IMM($REG)";
                        else
                            return "unknown";
                    }));

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is OperandFormat && Equals((OperandFormat) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] OperandFormat other)
            => OperandTypes.SequenceEqual(other.OperandTypes);

        /// <inheritdoc />
        public override int GetHashCode()
            => OperandTypes.Aggregate(0, (a, x) => (a.GetHashCode()*397) ^ x.GetHashCode());
    }
}