using System.Collections.Generic;
using JetBrains.Annotations;

namespace Assembler.Constants {
    class Registers {

        // Static Fields //////////////////////////////////////////////////////

        /// <summary>
        ///     The mappings of register usage convention name to their numbers.
        /// </summary>
        private static readonly Dictionary<string, int> REGISTER_NAME_MAPPINGS
            = new Dictionary<string, int> {
                {"zero"  ,  0},
                {"at"    ,  1},
                {"t0"    ,  2},
                {"t1"    ,  3},
                {"t2"    ,  4},
                {"t3"    ,  5},
                {"t4"    ,  6},
                {"t5"    ,  7},
                {"s0"    ,  8},
                {"ar0"   ,  9},
                {"ar1"   , 10},
                {"ar2"   , 11},
                {"ar3"   , 12},
                {"sp"    , 13},
                {"pc"    , 14},
                {"status", 15},
            };

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     The number of registers in the architecture.
        /// </summary>
        public static int NumberOfRegisters { get; } = REGISTER_NAME_MAPPINGS.Count;

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        /// Check if the given number is valid for specifiying a register.
        /// </summary>
        public static bool IsValidRegisterNumber(int num) => (num >= 0) && (num < NumberOfRegisters);

        /// <summary>
        /// Get the number of a register based on it's usage convention name.
        /// </summary>
        /// <param name="name">Case-insensitive register name.</param>
        /// <returns>Null if no register exists with the given name.</returns>
        [CanBeNull]
        public static int? NameToNumber([NotNull] string name) {
            name = name.ToLower();
            if (REGISTER_NAME_MAPPINGS.ContainsKey(name))
                return REGISTER_NAME_MAPPINGS[name];
            else
                return null;
        }
    }
}