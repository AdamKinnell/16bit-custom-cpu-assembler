using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using JetBrains.Annotations;

namespace Assembler.Registries {

    /// <summary>
    ///     Contains a list of all valid instructions for an architecture.
    /// </summary>
    public class InstructionRegistry {

        // Fields /////////////////////////////////////////////////////////////

        [NotNull] private readonly Dictionary<InstructionFormat, NativeInstruction> registry
            = new Dictionary<InstructionFormat, NativeInstruction>();

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Register the given instruction.
        /// </summary>
        /// <exception cref="InvalidOperationException"> </exception>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Register([NotNull] NativeInstruction instruction) {
            if (IsRegistered(instruction.Format))
                throw new InvalidOperationException("An instruction with this format is already registered.");
            registry.Add(instruction.Format, instruction);
        }

        /// <summary>
        ///     Check if an instruction of the given format has been registered.
        /// </summary>
        public bool IsRegistered([NotNull] InstructionFormat format)
            => registry.ContainsKey(format);

        /// <summary>
        ///     Find the registered native instruction with the given format.
        /// </summary>
        /// <returns> Null if no instruction of the given format has been registered. </returns>
        [CanBeNull]
        public NativeInstruction Find([NotNull] InstructionFormat format) {
            NativeInstruction instruction;
            registry.TryGetValue(format, out instruction);
            return instruction;
        }
    }
}