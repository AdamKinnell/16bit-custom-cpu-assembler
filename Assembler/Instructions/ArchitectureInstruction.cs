using System;
using Assembler.Instructions.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    /// 
    /// </summary>
    class InstructionBuilder {
        private Func<Int32, int> function_delegate;
        private Func<Int32, int> immediate_delegate;

        // Fields /////////////////////////////////////////////////////////////

        private string mnemonic;
        private Func<Int32, int> opcode_delegate;
        private Func<Int32, int> r1_delegate;
        private Func<Int32, int> r2_delegate;

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Set the mnemonic of this instruction.
        /// </summary>
        [NotNull]
        public InstructionBuilder Mnemonic([NotNull] string mnemonic) {
            this.mnemonic = mnemonic;
            return this;
        }

        /// <summary>
        ///     Define the function that generates the Opcode field for this instruction.
        /// </summary>
        [NotNull]
        public InstructionBuilder OpcodeField([NotNull] Func<Int32, int> func) {
            opcode_delegate = func;
            return this;
        }

        /// <summary>
        ///     Define the function that generates the Function field for this instruction.
        /// </summary>
        [NotNull]
        public InstructionBuilder FunctionField([NotNull] Func<Int32, int> func) {
            function_delegate = func;
            return this;
        }

        /// <summary>
        ///     Define the function that generates the R1 field for this instruction.
        /// </summary>
        [NotNull]
        public InstructionBuilder R1Field([NotNull] Func<Int32, int> func) {
            r1_delegate = func;
            return this;
        }

        /// <summary>
        ///     Define the function that generates the R2 field for this instruction.
        /// </summary>
        [NotNull]
        public InstructionBuilder R2Field([NotNull] Func<Int32, int> func) {
            r2_delegate = func;
            return this;
        }

        /// <summary>
        ///     Define the function that generates the Immediate field for this instruction.
        /// </summary>
        [NotNull]
        public InstructionBuilder ImmediateField([NotNull] Func<Int32, int> func) {
            immediate_delegate = func;
            return this;
        }

        /// <summary>
        ///     Builds an instruction with the given parameters.
        /// </summary>
        [NotNull]
        public ArchitectureInstruction Build() {
            if (mnemonic == null)
                throw new NullReferenceException("Mnemonic not given.");
            if ((opcode_delegate == null) ||
                (function_delegate == null) ||
                (r1_delegate == null) ||
                (r2_delegate == null) ||
                (immediate_delegate == null))
                throw new NullReferenceException("At least 1 delegate not given");

            return new ArchitectureInstruction(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class ArchitectureInstruction {

        // Fields /////////////////////////////////////////////////////////////

        // Constructors ///////////////////////////////////////////////////////

        public ArchitectureInstruction([NotNull] InstructionBuilder builder) {}

        public ArchitectureInstruction([NotNull] string mnemonic, [NotNull] OperandFormat operand_format) {
            Mnemonic = mnemonic;
            OperandFormat = operand_format;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary> The mnemonic for this instruction. </summary>
        [NotNull] public string Mnemonic { get; }

        /// <summary> The format of this instruction's operands. </summary>
        [NotNull] public OperandFormat OperandFormat { get; }

        // Implemented Functions //////////////////////////////////////////////
    }
}