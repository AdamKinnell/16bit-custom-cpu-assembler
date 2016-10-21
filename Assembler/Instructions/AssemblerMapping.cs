﻿using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Builds a mapping which generates instruction
    ///     fields from a list of operands.
    /// </summary>
    public class AssemblerMappingBuilder {

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

        /// <summary> Define a static value for the Opcode field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Opcode(Int32 value) {
            Opcode(_ => value);
            return this;
        }

        /// <summary> Define the function that generates the Function field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Function([NotNull] Func<OperandList, int> func) {
            function_delegate = func;
            return this;
        }

        /// <summary> Define a static value for the Function field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Function(Int32 value) {
            Function(_ => value);
            return this;
        }

        /// <summary> Define the function that generates the R1 field. </summary>
        [NotNull]
        public AssemblerMappingBuilder R1([NotNull] Func<OperandList, int> func) {
            r1_delegate = func;
            return this;
        }

        /// <summary> Define a static value for the R1 field. </summary>
        [NotNull]
        public AssemblerMappingBuilder R1(Int32 value) {
            R1(_ => value);
            return this;
        }

        /// <summary> Define the function that generates the R2 field. </summary>
        [NotNull]
        public AssemblerMappingBuilder R2([NotNull] Func<OperandList, int> func) {
            r2_delegate = func;
            return this;
        }

        /// <summary> Define a static value for the R2 field. </summary>
        [NotNull]
        public AssemblerMappingBuilder R2(Int32 value) {
            R2(_ => value);
            return this;
        }

        /// <summary> Define the function that generates the Immediate field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Immediate([NotNull] Func<OperandList, int> func) {
            immediate_delegate = func;
            return this;
        }

        /// <summary> Define a static value for the Immediate field. </summary>
        [NotNull]
        public AssemblerMappingBuilder Immediate(Int32 value) {
            Immediate(_ => value);
            return this;
        }

        /// <summary>
        ///     Check if all necessary fields have been provided.
        /// </summary>
        public bool IsValid() =>
            (opcode_delegate != null) &&
            (function_delegate != null) &&
            (r1_delegate != null) &&
            (r2_delegate != null) &&
            (immediate_delegate != null);

        /// <summary>
        ///     Builds an instruction with the given parameters.
        /// </summary>
        /// <exception cref="InvalidOperationException"> If any of the mappings are unspecified. </exception>
        [NotNull]
        public InstructionFieldMapping Build() {
            if (IsValid())
                return new InstructionFieldMapping(this);
            else
                throw new InvalidOperationException("All mappings must be specified.");
        }

        // Nested Class ///////////////////////////////////////////////////////

        /// <summary>
        ///     Represents mappings from operands
        ///     to the fields in the instruction machine code.
        /// </summary>
        public class InstructionFieldMapping {

            // Fields /////////////////////////////////////////////////////////////

            private readonly AssemblerMappingBuilder builder;

            // Constructors ///////////////////////////////////////////////////////

            /// <summary>
            /// 
            /// </summary>
            /// <param name="builder"> </param>
            public InstructionFieldMapping([NotNull] AssemblerMappingBuilder builder) {
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
            public Int32 AssembleFromOperands([NotNull] OperandList operands)
                => new MachineCodeBuilder()
                    .Opcode(builder.opcode_delegate(operands))
                    .Function(builder.function_delegate(operands))
                    .R1(builder.r1_delegate(operands))
                    .R2(builder.r2_delegate(operands))
                    .Immediate(builder.immediate_delegate(operands))
                    .Build();
        }
    }

}