using System;
using Assembler.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Instructions {

    [TestClass]
    public class MachineCodeBuilderTests {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenNoFieldsSet_Build_ShouldThrow()
            => new MachineCodeBuilder().Build();

        [TestMethod]
        public void GivenAllFieldBitsOne_BuiltMachineCode_ShouldHaveAllBitsOne() {
            const Int32 ALL_BITS_ONE = unchecked ((int) 0xFFFFFFFF);
            Assert.AreEqual(ALL_BITS_ONE, new MachineCodeBuilder()
                                .Opcode(ALL_BITS_ONE)
                                .Function(ALL_BITS_ONE)
                                .R1(ALL_BITS_ONE)
                                .R2(ALL_BITS_ONE)
                                .Immediate(ALL_BITS_ONE)
                                .Build());
        }

        [TestMethod]
        public void GivenAllFieldBitsZero_BuiltMachineCode_ShouldHaveAllBitsZero() {
            const Int32 ALL_BITS_ZERO = 0x00000000;
            Assert.AreEqual(ALL_BITS_ZERO, new MachineCodeBuilder()
                                .Opcode(ALL_BITS_ZERO)
                                .Function(ALL_BITS_ZERO)
                                .R1(ALL_BITS_ZERO)
                                .R2(ALL_BITS_ZERO)
                                .Immediate(ALL_BITS_ZERO)
                                .Build());
        }

        [TestMethod]
        public void GivenAlternatingFieldBits_BuiltMachineCode_ShouldHaveAllFieldsSetCorrectly() {
            const Int32 ALL_BITS_ONE = unchecked ((int) 0xFFFFFFFF);
            const Int32 ALL_BITS_ZERO = 0x00000000;

            Int32 expected = Convert.ToInt32("1111111111111111 0000 1111 000 11111".Replace(" ", ""), 2);
            Assert.AreEqual(expected, new MachineCodeBuilder()
                                .Opcode(ALL_BITS_ONE)
                                .Function(ALL_BITS_ZERO)
                                .R1(ALL_BITS_ONE)
                                .R2(ALL_BITS_ZERO)
                                .Immediate(ALL_BITS_ONE)
                                .Build());
        }

        [TestMethod]
        public void GivenReverseAlternatingFieldBits_BuiltMachineCode_ShouldHaveAllFieldsSetCorrectly() {
            const Int32 ALL_BITS_ONE = unchecked ((int) 0xFFFFFFFF);
            const Int32 ALL_BITS_ZERO = 0x00000000;

            Int32 expected = Convert.ToInt32("0000000000000000 1111 0000 111 00000".Replace(" ", ""), 2);
            Assert.AreEqual(expected, new MachineCodeBuilder()
                                .Opcode(ALL_BITS_ZERO)
                                .Function(ALL_BITS_ONE)
                                .R1(ALL_BITS_ZERO)
                                .R2(ALL_BITS_ONE)
                                .Immediate(ALL_BITS_ZERO)
                                .Build());
        }

        [TestMethod]
        public void GivenEachFieldIsOne_BuiltMachineCode_ShouldHaveBitSetOnRightSideOfField() {
            Int32 expected = Convert.ToInt32("0000000000000001 0001 0001 001 00001".Replace(" ", ""), 2);
            Assert.AreEqual(expected, new MachineCodeBuilder()
                                .Opcode(1)
                                .Function(1)
                                .R1(1)
                                .R2(1)
                                .Immediate(1)
                                .Build());
        }
    }
}