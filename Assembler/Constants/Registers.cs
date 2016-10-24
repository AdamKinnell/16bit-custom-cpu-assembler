using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Assembler.Constants {
    public class Registers {

        // Enums //////////////////////////////////////////////////////////////

        /// <summary>
        ///     The mappings of register usage convention names to their numbers.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public enum RegisterNumber {
            ZERO,
            AT,
            T0,
            T1,
            T2,
            T3,
            T4,
            T5,
            S0,
            AR0,
            AR1,
            AR2,
            AR3,
            SP,
            PC,
            STATUS
        }

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Check if the given number is valid for specifiying a register.
        /// </summary>
        public static bool IsValidRegisterNumber(int num)
            => Enum.IsDefined(typeof(RegisterNumber), num);

        /// <summary>
        ///     Get the number of a register based on it's usage convention name.
        /// </summary>
        /// <param name="name"> Case-insensitive register name. </param>
        /// <returns> Null if no register exists with the given name. </returns>
        [CanBeNull]
        public static int? NameToNumber([NotNull] string name) {
            RegisterNumber register;
            if (Enum.TryParse(name, true, out register)) {
                return (int?) register;
            } else {
                return null;
            }
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The number of registers in the architecture.
        /// </summary>
        public static int NumberOfRegisters
            => Enum.GetNames(typeof(RegisterNumber)).Length;
    }
}