using System;
using Assembler.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Instructions {

    [TestClass]
    public class MachineCodeBuilderTests {

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GivenNoFieldsSet_Build_ShouldThrow()
            => new MachineCodeBuilder().Build();

        [TestMethod]
        public void GivenAllFieldBitsOne_MachineCode_ShouldHaveAllBitsOne() {
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
        public void GivenAllFieldBitsZero_MachineCode_ShouldHaveAllBitsZero() {
            const Int32 ALL_BITS_ZERO = 0x00000000;
            Assert.AreEqual(ALL_BITS_ZERO, new MachineCodeBuilder()
                                .Opcode(ALL_BITS_ZERO)
                                .Function(ALL_BITS_ZERO)
                                .R1(ALL_BITS_ZERO)
                                .R2(ALL_BITS_ZERO)
                                .Immediate(ALL_BITS_ZERO)
                                .Build());
        }
    }
}