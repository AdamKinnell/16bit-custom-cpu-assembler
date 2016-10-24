using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using Assembler.Instructions.Definitions;
using JetBrains.Annotations;

namespace Assembler.Registries {

    /// <summary>
    ///     Contains a list of all valid instructions for an architecture.
    /// </summary>
    public class InstructionRegistry {

        // Fields /////////////////////////////////////////////////////////////

        [NotNull] private readonly Dictionary<InstructionFormat, IInstructionDefinition> registry
            = new Dictionary<InstructionFormat, IInstructionDefinition>();

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Register the given instruction_definition.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If an instruction definition of the given format has already been registered.
        /// </exception>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Register([NotNull] IInstructionDefinition instruction_definition) {
            if (IsRegistered(instruction_definition.Format))
                throw new InvalidOperationException("An instruction_definition with this format is already registered.");
            registry.Add(instruction_definition.Format, instruction_definition);
        }

        /// <summary>
        ///     Check if an instruction_definition of the given format has been registered.
        /// </summary>
        public bool IsRegistered([NotNull] InstructionFormat format)
            => registry.ContainsKey(format);

        /// <summary>
        ///     Find the registered native instruction_definition with the given format.
        /// </summary>
        /// <returns> Null if no instruction definition of the given format has been registered. </returns>
        [CanBeNull]
        public IInstructionDefinition TryFind([NotNull] InstructionFormat format) {
            IInstructionDefinition instruction_definition;
            registry.TryGetValue(format, out instruction_definition);
            return instruction_definition;
        }

        /// <summary>
        ///     Find the registered native instruction_definition with the given format.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If no instruction definition of the given format has been registered.
        /// </exception>
        [NotNull]
        public IInstructionDefinition Find([NotNull] InstructionFormat format) {
            IInstructionDefinition instruction_definition;
            registry.TryGetValue(format, out instruction_definition);
            if (instruction_definition != null)
                return instruction_definition;
            else
                throw new InvalidOperationException("An instruction defintion of the given format has not been registered.");
        }
    }
}