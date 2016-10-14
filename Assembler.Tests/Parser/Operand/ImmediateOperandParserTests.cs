using System;
using System.Linq;
using Assembler.Parser.Operand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Parser.Operand {

    [TestClass]
    public class ImmediateOperandParserTests {

        [TestMethod]
        public void Given16bitSignedDecimalString_ConvertToOperand_ExpectSuccess() {
            var parser = new ImmediateOperandParser();
            var input = new[] {"-32768", "-256", "-25", "-1", "0", "1", "25", "255", "32767"};
            var expected = new[] {-32768, -256, -25, -1, 0, 1, 25, 255, 32767};

            var actual = input.Select(x => parser.Parse(x).Value).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Given16bitHexString_ConvertToOperand_ExpectSuccess() {
            var parser = new ImmediateOperandParser();
            var input = new[] {"0x0", "0x0000", "0x0001", "0x1001", "0xFFFF"};
            var expected = new[] {0x0, 0x0000, 0x0001, 0x1001, 0xFFFF};

            var actual = input.Select(x => parser.Parse(x).Value).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Given16bitBinaryString_ConvertToOperand_ExpectSuccess() {
            var parser = new ImmediateOperandParser();
            var input = new[] {"0b0", "0b00000000", "0b0000000000000000", "0b1111111111111111"};
            var expected = new[] {0x0, 0x0, 0x0, 0xFFFF};

            var actual = input.Select(x => parser.Parse(x).Value).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenInvalidInput_ConvertToOperand_ExpectException()
            => new ImmediateOperandParser().Parse("9xfL;^%12313P");
    }
}