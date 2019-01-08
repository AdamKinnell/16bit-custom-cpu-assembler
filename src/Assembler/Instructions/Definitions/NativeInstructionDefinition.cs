using System;
using Assembler.Operands;
using JetBrains.Annotations;

namespace Assembler.Instructions.Definitions {

    /// <summary>
    ///     Defines a native instruction with a name (mnemonic),
    ///     operand format, and a direct mapping to machine code.
    /// </summary>
    public class NativeInstructionDefinition : IInstructionDefinition {

        // Fields /////////////////////////////////////////////////////////////

        [NotNull] private readonly InstructionFieldMappingBuilder.InstructionFieldMapping field_mapping;

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from mnemonic, operand formats, and mappings.
        /// </summary>
        public NativeInstructionDefinition([NotNull] string mnemonic, [NotNull] OperandFormat operand_format,
                                           [NotNull] InstructionFieldMappingBuilder.InstructionFieldMapping mapping) {
            Format = new InstructionFormat(mnemonic, operand_format);
            field_mapping = mapping;
        }

        /// <summary>
        ///     Construct directly from instruction format and mappings.
        /// </summary>
        public NativeInstructionDefinition([NotNull] InstructionFormat format,
                                           [NotNull] InstructionFieldMappingBuilder.InstructionFieldMapping mapping) {
            Format = format;
            field_mapping = mapping;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <inheritdoc />
        public InstructionFormat Format { get; }

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Assemble this instruction with the given operands.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     If the format of the operands given is different to the format expected.
        /// </exception>
        public MachineCode AssembleWithOperands(OperandList operands) {
            if (operands.Format.Equals(Format.OperandFormat))
                return field_mapping.AssembleFromOperands(operands);
            else
                throw new ArgumentException("The given operands are not in the expected format.");
        }
    }
}