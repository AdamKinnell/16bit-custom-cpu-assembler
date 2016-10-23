using System;
using System.Collections.Generic;
using System.Linq;
using Assembler.Constants;
using Assembler.Operands;
using Assembler.Operands.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Operands {
    [TestClass]
    public class OperandFormatTests {

        [TestMethod]
        public void GivenOperandsAndTheirTypes_BothConstructorsReturnEqualObjects() {

            var expected_types = new List<Type> {
                typeof(RegisterOperand),
                typeof(ImmediateOperand),
                typeof(BaseOffsetOperand)
            };

            var operands = new List<IOperand> {
                new RegisterOperand(Registers.RegisterNumber.T0),
                new ImmediateOperand(0xFF),
                new BaseOffsetOperand(
                    new RegisterOperand(Registers.RegisterNumber.T0),
                    new ImmediateOperand(0xFF))
            };

            // Sanity check.
            CollectionAssert.AreEqual(expected_types, operands.Select(x => x.GetType()).ToList());

            // Equality operator should return true.
            Assert.AreEqual(new OperandFormat(operands), new OperandFormat(expected_types));

            // Types in the format should match those given.
            CollectionAssert.AreEqual(expected_types, new OperandFormat(operands).OperandTypes.ToList());
        }
    }
}