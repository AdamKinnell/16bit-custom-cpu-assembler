using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    /// 
    /// </summary>
    class AssemblerMappingBuilder {

        // Fields /////////////////////////////////////////////////////////////

        private Func<OperandList, int> opcode_delegate;
        private Func<OperandList, int> r1_delegate;
        private Func<OperandList, int> r2_delegate;
        private Func<OperandList, int> function_delegate;
        private Func<OperandList, int> immediate_delegate;

        // Functions //////////////////////////////////////////////////////////

        /// <summary> Define the function that generates the Opcode field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Opcode([NotNull] Func<OperandList, int> func) {
            opcode_delegate = func;
            return this;
        }

        /// <summary> Define the function that generates the Function field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Function([NotNull] Func<OperandList, int> func) {
            function_delegate = func;
            return this;
        }

        /// <summary> Define the function that generates the R1 field. </summary>
        [NotNull]
        public AssemblerMappingBuilder R1([NotNull] Func<OperandList, int> func) {
            r1_delegate = func;
            return this;
        }

        /// <summary> Define the function that generates the R2 field. </summary>
        [NotNull]
        public AssemblerMappingBuilder R2([NotNull] Func<OperandList, int> func) {
            r2_delegate = func;
            return this;
        }

        /// <summary> Define the function that generates the Immediate field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Immediate([NotNull] Func<OperandList, int> func) {
            immediate_delegate = func;
            return this;
        }

        /// <summary>
        ///     Builds an instruction with the given parameters.
        /// </summary>
        [NotNull]
        public AssemblerMapping Build() {
            if ((opcode_delegate == null) ||
                (function_delegate == null) ||
                (r1_delegate == null) ||
                (r2_delegate == null) ||
                (immediate_delegate == null))
                throw new NullReferenceException("All mappings must be specified.");

            return new AssemblerMapping(this);
        }

        /// <summary>
        ///     Assembles an instruction (i.e. generates the fields)
        /// </summary>
        public class AssemblerMapping {

            // Static Fields //////////////////////////////////////////////////////

            // 5-bit Opcode.
            private static readonly Int32 OPCODE_OFFSET = 0;

            private static readonly Int32 OPCODE_MASK
                = Convert.ToInt32("0000000000000000 0000 0000 000 11111", 2);

            // 3-bit Function
            private static readonly Int32 FUNCTION_OFFSET = OPCODE_OFFSET + 5;

            private static readonly Int32 FUNCTION_MASK
                = Convert.ToInt32("0000000000000000 0000 0000 111 00000", 2);

            // 4-bit Register
            private static readonly Int32 R1_OFFSET = FUNCTION_OFFSET + 3;

            private static readonly Int32 R1_MASK
                = Convert.ToInt32("0000000000000000 0000 1111 000 00000", 2);

            // 4-bit Register
            private static readonly Int32 R2_OFFSET = R1_OFFSET + 4;

            private static readonly Int32 R2_MASK
                = Convert.ToInt32("0000000000000000 1111 0000 000 00000", 2);

            // 16-bit Immediate
            private static readonly Int32 IMMEDIATE_OFFSET = R2_OFFSET + 4;

            private static readonly Int32 IMMEDIATE_MASK
                = Convert.ToInt32("1111111111111111 0000 0000 000 00000", 2);

            // Fields /////////////////////////////////////////////////////////////

            private readonly AssemblerMappingBuilder builder;

            // Constructors ///////////////////////////////////////////////////////

            public AssemblerMapping([NotNull] AssemblerMappingBuilder builder) {
                this.builder = builder;
            }

            // Functions //////////////////////////////////////////////////////////

            /// <summary>
            ///     Using the internal mapping, generate the machine code for
            ///     the instruction with the given operands.
            /// </summary>
            /// <param name="operands"> </param>
            /// <returns> </returns>
            [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
            public Int32 AssembleFromOperands([NotNull] OperandList operands) {
                var machine_code = 0x0;

                machine_code |= (builder.opcode_delegate(operands) << OPCODE_OFFSET) & OPCODE_MASK;
                machine_code |= (builder.function_delegate(operands) << FUNCTION_OFFSET) & FUNCTION_MASK;
                machine_code |= (builder.r1_delegate(operands) << R1_OFFSET) & R1_MASK;
                machine_code |= (builder.r2_delegate(operands) << R2_OFFSET) & R2_MASK;
                machine_code |= (builder.immediate_delegate(operands) << IMMEDIATE_OFFSET) & IMMEDIATE_MASK;

                return machine_code;
            }
        }
    }

}