using System.Collections.Generic;
using Assembler.Instructions;
using JetBrains.Annotations;

namespace Assembler.Registries {

    /// <summary>
    ///     Contains a list of all valid instructions for an architecture.
    /// </summary>
    class InstructionRegistry {

        // Fields /////////////////////////////////////////////////////////////

        private readonly Dictionary<string, ArchitectureInstruction> registered_instructions
            = new Dictionary<string, ArchitectureInstruction>();

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instruction"> </param>
        public void Register([NotNull] ArchitectureInstruction instruction)
            // ReSharper disable once PossibleNullReferenceException
            => registered_instructions.Add(instruction.Mnemonic, instruction);
    }
}