using Assembler.Instructions.Operands.OperandTypes;
using JetBrains.Annotations;

namespace Assembler.Instructions.Operands {

    // Interface
    ///////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///     Marker interface representing a list of operands of some format.
    /// </summary>
    public interface IOperandList {

        /// <summary>
        ///     Get the format of this operand list.
        /// </summary>
        OperandFormatType GetFormat();
    }

    // Classes
    ///////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///     An empty operand list.
    /// </summary>
    public class OperandListEmpty : IOperandList {

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.FormatEmpty;
    }

    /// <summary>
    ///     A single register operand.
    /// </summary>
    public class OperandList1R : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandList1R([NotNull] RegisterOperand register) {
            Register = register;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public RegisterOperand Register { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.Format1R;
    }

    /// <summary>
    ///     Two register operands.
    /// </summary>
    public class OperandList2R : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandList2R([NotNull] RegisterOperand register_a, [NotNull] RegisterOperand register_b) {
            RegisterA = register_a;
            RegisterB = register_b;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public RegisterOperand RegisterA { get; }
        [NotNull] public RegisterOperand RegisterB { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.Format2R;
    }

    /// <summary>
    ///     A single immediate operand.
    /// </summary>
    public class OperandList1I : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandList1I([NotNull] ImmediateOperand immediate) {
            Immediate = immediate;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public ImmediateOperand Immediate { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.Format1I;
    }

    /// <summary>
    ///     One immediate and one register operand.
    /// </summary>
    public class OperandList1R1I : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandList1R1I([NotNull] RegisterOperand register, [NotNull] ImmediateOperand immediate) {
            Immediate = immediate;
            Register = register;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public RegisterOperand Register { get; }
        [NotNull] public ImmediateOperand Immediate { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.Format1R1I;
    }

    /// <summary>
    ///     One immediate and two register operands.
    /// </summary>
    public class OperandList2R1I : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandList2R1I([NotNull] ImmediateOperand immediate, [NotNull] RegisterOperand register_b, [NotNull] RegisterOperand register_a) {
            Immediate = immediate;
            RegisterB = register_b;
            RegisterA = register_a;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public RegisterOperand RegisterA { get; }
        [NotNull] public RegisterOperand RegisterB { get; }
        [NotNull] public ImmediateOperand Immediate { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.Format2R1I;
    }

    /// <summary>
    ///     A base register plus an offset.
    /// </summary>
    public class OperandListOffset : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandListOffset([NotNull] RegisterOperand base_register, [NotNull] ImmediateOperand offset) {
            BaseRegister = base_register;
            Offset = offset;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public RegisterOperand BaseRegister { get; }
        [NotNull] public ImmediateOperand Offset { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.FormatOffset;
    }

    /// <summary>
    ///     A base register plus an offset, with an additional register.
    /// </summary>
    public class OperandList1ROffset : IOperandList {

        // Constructors ///////////////////////////////////////////////////////
        public OperandList1ROffset([NotNull] RegisterOperand register, [NotNull] RegisterOperand base_register, [NotNull] ImmediateOperand offset) {
            Register = register;
            BaseRegister = base_register;
            Offset = offset;
        }

        // Properties /////////////////////////////////////////////////////////
        [NotNull] public RegisterOperand Register { get; }
        [NotNull] public RegisterOperand BaseRegister { get; }
        [NotNull] public ImmediateOperand Offset { get; }

        // Implemented Functions //////////////////////////////////////////////
        /// <inheritdoc />
        public OperandFormatType GetFormat() => OperandFormatType.Format1ROffset;
    }

}