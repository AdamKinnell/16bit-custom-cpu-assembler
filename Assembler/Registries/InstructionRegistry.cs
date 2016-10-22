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

        private readonly Dictionary<string, NativeInstruction> registry
            = new Dictionary<string, NativeInstruction>();

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Register the given instruction.
        /// </summary>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Register([NotNull] NativeInstruction instruction)
            => registry.Add(instruction.Mnemonic, instruction);

        /// <summary>
        ///     Find the registered native instruction with the given format.
        /// </summary>
        /// <returns> Null if no instruction of the given format has been registered. </returns>
        [CanBeNull]
        public NativeInstruction Find([NotNull] SourceInstruction instruction) =>
            registry.ContainsKey(instruction.Mnemonic)
                ? registry[instruction.Mnemonic] : null;
    }
}