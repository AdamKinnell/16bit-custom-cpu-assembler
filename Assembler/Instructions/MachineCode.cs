using System;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Represents the binary format of an instruction.
    /// </summary>
    public class MachineCode {

        // Fields /////////////////////////////////////////////////////////////

        private readonly Int32 instruction;

        // Constructors ///////////////////////////////////////////////////////

        /// <summary>
        ///     Construct from the binary representation of an instruction.
        /// </summary>
        public MachineCode(Int32 instruction) {
            this.instruction = instruction;
        }

        // Properties /////////////////////////////////////////////////////////

        /// <summary>
        ///     Get the native integer representation of the machine code.
        /// </summary>
        public Int32 AsInteger()
            => instruction;

        /// <summary>
        ///     Get the little-endian representation of the machine code.
        /// </summary>
        [NotNull]
        public byte[] AsLittleEndianBytes() {
            var bytes = BitConverter.GetBytes(instruction);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return bytes;
        }

        /// <summary>
        ///     Get the big-endian representation of the machine code.
        /// </summary>
        [NotNull]
        public byte[] AsBigEndianBytes() {
            var bytes = BitConverter.GetBytes(instruction);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return bytes;
        }

    }
}