using System;
using Assembler.Constants;
using Assembler.Instructions;
using Assembler.Instructions.Operands.Types;
using Assembler.Lexer.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Lexer.Tokenizer {

    [TestClass]
    public class SourceLineTokenizerTests {

        [TestMethod]
        public void GivenEmptyLine_TokenizeLine_ReturnsEmptyLine()
            => Assert.IsTrue(new SourceLineTokenizer().TokenizeLine("").IsEmpty);

        [TestMethod]
        public void GivenWhitespaceLine_TokenizeLine_ReturnsEmptyLine()
            => Assert.IsTrue(new SourceLineTokenizer().TokenizeLine("    ").IsEmpty);

        [TestMethod]
        public void GivenOnlyLabel_TokenizeLine_ReturnsOnlyLabel()
            => Assert.AreEqual(new TokenizedSourceLine("main", null, null),
                               new SourceLineTokenizer().TokenizeLine("main:"));

        [TestMethod]
        public void GivenOnlyMnemonic_TokenizeLine_ReturnsOnlyMnemonic()
            => Assert.AreEqual(new TokenizedSourceLine(new SourceInstruction("add")),
                               new SourceLineTokenizer().TokenizeLine("add"));

        [TestMethod]
        public void GivenOnlyComment_TokenizeLine_ReturnsOnlyComment()
            => Assert.AreEqual(new TokenizedSourceLine(null, null, "This is a comment"),
                               new SourceLineTokenizer().TokenizeLine("# This is a comment"));

        [TestMethod]
        public void TestFullLine() {
            var splitter = new SourceLineTokenizer();
            var expected = new TokenizedSourceLine(
                label: "_MaiN_",
                comment: ":$#$:comment:$#$:",
                instruction: new SourceInstruction(
                    "AnD",
                    new RegisterOperand(Registers.RegisterNumber.T0),
                    new RegisterOperand(Registers.RegisterNumber.T1),
                    new ImmediateOperand(3260)
                )
            );
            Assert.AreEqual(
                expected,
                splitter.TokenizeLine("_MaiN_: AnD $t0, $t1, 3260 #:$#$:comment:$#$:"));
            Assert.AreEqual(
                expected,
                splitter.TokenizeLine("_MaiN_:AnD $t0,$t1,3260#:$#$:comment:$#$:"));
            Assert.AreEqual(
                expected,
                splitter.TokenizeLine("  _MaiN_  :  AnD  $t0  ,  $t1  ,  3260  #  :$#$:comment:$#$:  "));
        }

        [TestMethod]
        public void GivenInstructionWithExtraFormatting_TokenizeLine_Succeeds() {
            var splitter = new SourceLineTokenizer();
            var expected = new TokenizedSourceLine(
                label: null, comment: null, instruction:
                new SourceInstruction(
                    "and",
                    new RegisterOperand(Registers.RegisterNumber.T0),
                    new RegisterOperand(Registers.RegisterNumber.T1)
                )
            );

            Assert.AreEqual(expected, splitter.TokenizeLine("and $t0 $t1"));
            Assert.AreEqual(expected, splitter.TokenizeLine("and $t0  $t1"));
            Assert.AreEqual(expected, splitter.TokenizeLine("and $t0, $t1"));
            Assert.AreEqual(expected, splitter.TokenizeLine("and $t0 , $t1"));
            Assert.AreEqual(expected, splitter.TokenizeLine("and $t0 ,$t1"));
            Assert.AreEqual(expected, splitter.TokenizeLine("and $t0,$t1"));
        }

        [TestMethod]
        public void GivenInstructionWithImmediateDecimalOperand_TokenizeLine_Succeeds()
            => Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "add", new ImmediateOperand(100))),
                new SourceLineTokenizer().TokenizeLine("add 100"));

        [TestMethod]
        public void GivenInstructionWithImmediateHexOperand_TokenizeLine_Succeeds()
            => Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "add", new ImmediateOperand(0x019ABCF))),
                new SourceLineTokenizer().TokenizeLine("add 0x019ABCF"));

        [TestMethod]
        public void GivenInstructionWithImmediateBinaryOperand_TokenizeLine_Succeeds()
            => Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "add", new ImmediateOperand(0x95))),
                new SourceLineTokenizer().TokenizeLine("add 0b10010101"));

        [TestMethod]
        public void GivenInstructionWithBaseOffsetOperand_TokenizeLine_Succeeds()
            => Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "lw", new BaseOffsetOperand(
                            new RegisterOperand(Registers.RegisterNumber.T0),
                            new ImmediateOperand(0x8)))),
                new SourceLineTokenizer().TokenizeLine("lw 0x8($t0)"));

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GivenInvalidLine_TokenizeLine_Throws()
            => new SourceLineTokenizer().TokenizeLine("67r56f");
    }
}