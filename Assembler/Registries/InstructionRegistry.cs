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

        [NotNull] private readonly Dictionary<InstructionFormat, NativeInstructionDefinition> registry
            = new Dictionary<InstructionFormat, NativeInstructionDefinition>();

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Register the given instruction_definition.
        /// </summary>
        /// <exception cref="InvalidOperationException"> </exception>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Register([NotNull] NativeInstructionDefinition instruction_definition) {
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
        /// <returns> Null if no instruction_definition of the given format has been registered. </returns>
        [CanBeNull]
        public NativeInstructionDefinition Find([NotNull] InstructionFormat format) {
            NativeInstructionDefinition instruction_definition;
            registry.TryGetValue(format, out instruction_definition);
            return instruction_definition;
        }
    }
}