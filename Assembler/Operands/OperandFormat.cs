﻿using System;
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
            OperandTypes = operands.Select(operand => operand.GetType());
        }

        /// <summary>
        ///     Construct directly from a list of types.
        /// </summary>
        /// <param name="operand_types"> Must be subclasses of IOperand. </param>
        /// <exception cref="ArgumentException"> If any type not subclass of IOperand. </exception>
        [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        public OperandFormat([NotNull, ItemNotNull] IReadOnlyCollection<Type> operand_types) {
            if (OperandTypes.Any(operand_type => !operand_type.IsSubclassOf(typeof(IOperand)))) {
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

        [NotNull, ItemNotNull] public IEnumerable<Type> OperandTypes { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is OperandFormat && Equals((OperandFormat) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] OperandFormat other)
            => OperandTypes.SequenceEqual(other.OperandTypes);

        /// <inheritdoc />
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}