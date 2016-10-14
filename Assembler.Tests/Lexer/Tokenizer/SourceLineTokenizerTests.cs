using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Constants;
using Assembler.Instructions;
using Assembler.Instructions.Operands.Types;
using Assembler.Lexer.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Lexer.Tokenizer {

    [TestClass]
    public class SourceLineTokenizerTests {

        [TestMethod]
        public void TestEmptyLine() {
            var splitter = new SourceLineTokenizer();
            Assert.IsTrue(splitter.TokenizeLine("").IsEmpty);
            Assert.IsTrue(splitter.TokenizeLine("    ").IsEmpty);
        }

        [TestMethod, SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public void TestSinglePartLine() {
            var splitter = new SourceLineTokenizer();

            Assert.AreEqual(
                new TokenizedSourceLine("main", null, null),
                splitter.TokenizeLine("main:"));
            Assert.AreEqual(
                new TokenizedSourceLine(new SourceInstruction("add")),
                splitter.TokenizeLine("add"));
            Assert.AreEqual(
                new TokenizedSourceLine(null, null, "This is a comment"),
                splitter.TokenizeLine("# This is a comment"));
        }

        [TestMethod, SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public void TestFullLine() {
            var splitter = new SourceLineTokenizer();
            var expected = new TokenizedSourceLine(
                label: "_MaiN_",
                comment: ":$#$:comment:$#$:",
                instruction: new SourceInstruction(
                    "AnD",
                    new RegisterOperand(Registers.NameToNumber("t0").Value),
                    new RegisterOperand(Registers.NameToNumber("t1").Value),
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

        [TestMethod, SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public void TestInstructionCleanup() {
            var splitter = new SourceLineTokenizer();
            var expected = new TokenizedSourceLine(
                label: null, comment: null, instruction:
                new SourceInstruction(
                    "and",
                    new RegisterOperand(Registers.NameToNumber("t0").Value),
                    new RegisterOperand(Registers.NameToNumber("t1").Value)
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
        public void TestImmediateOperands() {
            var splitter = new SourceLineTokenizer();

            // Decimal
            Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "add", new ImmediateOperand(100))),
                splitter.TokenizeLine("add 100"));

            // Hexadecimal
            Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "add", new ImmediateOperand(0x019ABCF))),
                splitter.TokenizeLine("add 0x019ABCF"));

            // Binary
            Assert.AreEqual(
                new TokenizedSourceLine(
                    new SourceInstruction(
                        "add", new ImmediateOperand(0x95))),
                splitter.TokenizeLine("add 0b10010101"));

            // Label
            // TODO
        }

        //[TestMethod]
        //public void TestBaseOffsetOperands() {
        //    var splitter = new SourceLineTokenizer();
        //    var expected = new ScannedSourceLine(null, "sub", new[] {"0xffe($t0)"}, null);

        //    Assert.AreEqual(expected, splitter.TokenizeLine("sub 0xffe($t0)"));
        //    Assert.AreEqual(expected, splitter.TokenizeLine("sub 0xffe ( $t0 ) "));
        //}

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestInvalidLine() {
            var splitter = new SourceLineTokenizer().TokenizeLine("67r56f");
        }
    }
}