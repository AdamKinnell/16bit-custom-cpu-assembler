using System.Collections.Generic;
using Assembler.Instructions;
using JetBrains.Annotations;

namespace Assembler.Registries {

    /// <summary>
    ///     Contains a list of all valid instructions for an architecture.
    /// </summary>
    class InstructionRegistry {

        // Fields /////////////////////////////////////////////////////////////

        private readonly Dictionary<string, NativeInstruction> registry
            = new Dictionary<string, NativeInstruction>();

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Register the given instruction.
        /// </summary>
        public void Register([NotNull] NativeInstruction instruction)
            // ReSharper disable once PossibleNullReferenceException
            => registry.Add(instruction.Mnemonic, instruction);
    }
}