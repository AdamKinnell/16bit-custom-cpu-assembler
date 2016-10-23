using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Assembler.Instructions {

    /// <summary>
    ///     Generates machine code for an instruction, given the values of each field.
    /// </summary>
    public class MachineCodeBuilder {

        // Static Fields //////////////////////////////////////////////////////

        // 5-bit Opcode.
        private static readonly Int32 OPCODE_OFFSET = 0;

        private static readonly Int32 OPCODE_MASK
            = Convert.ToInt32("0000000000000000 0000 0000 000 11111".Replace(" ", ""), 2);

        // 3-bit Function
        private static readonly Int32 FUNCTION_OFFSET = OPCODE_OFFSET + 5;

        private static readonly Int32 FUNCTION_MASK
            = Convert.ToInt32("0000000000000000 0000 0000 111 00000".Replace(" ", ""), 2);

        // 4-bit Register
        private static readonly Int32 R1_OFFSET = FUNCTION_OFFSET + 3;

        private static readonly Int32 R1_MASK
            = Convert.ToInt32("0000000000000000 0000 1111 000 00000".Replace(" ", ""), 2);

        // 4-bit Register
        private static readonly Int32 R2_OFFSET = R1_OFFSET + 4;

        private static readonly Int32 R2_MASK
            = Convert.ToInt32("0000000000000000 1111 0000 000 00000".Replace(" ", ""), 2);

        // 16-bit Immediate
        private static readonly Int32 IMMEDIATE_OFFSET = R2_OFFSET + 4;

        private static readonly Int32 IMMEDIATE_MASK
            = Convert.ToInt32("1111111111111111 0000 0000 000 00000".Replace(" ", ""), 2);

        // Fields /////////////////////////////////////////////////////////////

        private Int32? opcode;
        private Int32? function;
        private Int32? r1;
        private Int32? r2;
        private Int32? immediate;

        // Functions //////////////////////////////////////////////////////////

        /// <summary>
        ///     Set the value of the Opcode field to the lower 5 bits of the argument.
        /// </summary>
        [NotNull]
        public MachineCodeBuilder Opcode(Int32 value) {
            opcode = value;
            return this;
        }

        /// <summary>
        ///     Set the value of the Function field to the lower 3 bits of the argument.
        /// </summary>
        [NotNull]
        public MachineCodeBuilder Function(Int32 value) {
            function = value;
            return this;
        }

        /// <summary>
        ///     Set the value of the R1 field to the lower 4 bits of the argument.
        /// </summary>
        [NotNull]
        public MachineCodeBuilder R1(Int32 value) {
            r1 = value;
            return this;
        }

        /// <summary>
        ///     Set the value of the R2 field to the lower 4 bits of the argument.
        /// </summary>
        [NotNull]
        public MachineCodeBuilder R2(Int32 value) {
            r2 = value;
            return this;
        }

        /// <summary>
        ///     Set the value of the Immediate field to the lower 16 bits of the argument.
        /// </summary>
        [NotNull]
        public MachineCodeBuilder Immediate(Int32 value) {
            immediate = value;
            return this;
        }

        /// <summary>
        ///     Check if all necessary fields have been provided.
        /// </summary>
        public bool IsValid() =>
            (opcode != null) &&
            (function != null) &&
            (r1 != null) &&
            (r2 != null) &&
            (immediate != null);

        /// <summary>
        ///     Pack the values of the fields into a single 32-bit value.
        /// </summary>
        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        private Int32 PackAsInt32() {
            var machine_code = 0x0;
            machine_code |= (opcode.Value << OPCODE_OFFSET) & OPCODE_MASK;
            machine_code |= (function.Value << FUNCTION_OFFSET) & FUNCTION_MASK;
            machine_code |= (r1.Value << R1_OFFSET) & R1_MASK;
            machine_code |= (r2.Value << R2_OFFSET) & R2_MASK;
            machine_code |= (immediate.Value << IMMEDIATE_OFFSET) & IMMEDIATE_MASK;
            return machine_code;
        }

        /// <summary>
        ///     Generates the machine code from the values set.
        /// </summary>
        /// <exception cref="InvalidOperationException"> If any of the fields are unspecified. </exception>
        public Int32 BuildAsInt32() {
            if (IsValid())
                return PackAsInt32();
            else
                throw new InvalidOperationException("All fields must be specified.");
        }

        /// <summary>
        ///     Generates the machine code from the values set.
        /// </summary>
        /// <exception cref="InvalidOperationException"> If any of the fields are unspecified. </exception>
        [NotNull]
        public byte[] BuildAsLittleEndianBytes() {
            if (IsValid()) {
                var bytes = BitConverter.GetBytes(PackAsInt32());
                if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
                return bytes;
            } else
                throw new InvalidOperationException("All fields must be specified.");
        }

        /// <summary>
        ///     Generates the machine code from the values set.
        /// </summary>
        /// <exception cref="InvalidOperationException"> If any of the fields are unspecified. </exception>
        [NotNull]
        public byte[] BuildAsBigEndianBytes() {
            if (IsValid()) {
                var bytes = BitConverter.GetBytes(PackAsInt32());
                if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
                return bytes;
            } else
                throw new InvalidOperationException("All fields must be specified.");
        }
    }
}