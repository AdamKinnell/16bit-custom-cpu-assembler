using System;
using Assembler.Lexer.Scanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Lexer.Scanner {
    [TestClass]
    public class SourceLineScannerTests {

        [TestMethod]
        public void TestEmptyLine() {
            var splitter = new SourceLineScanner();
            Assert.IsTrue(splitter.TokenizeLine("").IsEmpty);
            Assert.IsTrue(splitter.TokenizeLine("    ").IsEmpty);
        }

        [TestMethod]
        public void TestSinglePartLine() {
            var splitter = new SourceLineScanner();

            Assert.AreEqual(
                new ScannedSourceLine("main", null, null, null),
                splitter.TokenizeLine("main:"));
            Assert.AreEqual(
                new ScannedSourceLine(null, "add", null, null),
                splitter.TokenizeLine("add"));
            Assert.AreEqual(
                new ScannedSourceLine(null, null, null, "This is a comment"),
                splitter.TokenizeLine("# This is a comment"));
        }

        [TestMethod]
        public void TestFullLine() {
            var splitter = new SourceLineScanner();
            var expected = new ScannedSourceLine(
                label: "_MaiN_",
                mnemonic: "AnD",
                operands: new[] {"$t0", "$t1", "3260"},
                comment: ":$#$:comment:$#$:"
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
        public void TestInstructionCleanup() {
            var splitter = new SourceLineScanner();
            var expected = new ScannedSourceLine(
                label: null,
                mnemonic: "and",
                operands: new[] {"$t0", "$t1"},
                comment: null
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
            var splitter = new SourceLineScanner();

            // Decimal
            Assert.AreEqual(
                new ScannedSourceLine(null, "add", new[] {"100"}, null),
                splitter.TokenizeLine("add 100"),
                "Decimal shall be an immediate");

            // Hexadecimal
            Assert.AreEqual(
                new ScannedSourceLine(null, "add", new[] {"0x019ABCF"}, null),
                splitter.TokenizeLine("add 0x019ABCF"),
                "Hexadecimal shall be an immediate");

            // Binary
            Assert.AreEqual(
                new ScannedSourceLine(null, "add", new[] {"0b10010101"}, null),
                splitter.TokenizeLine("add 0b10010101"),
                "Binary shall be an immediate");

            // Label
            Assert.AreEqual(
                new ScannedSourceLine(null, "add", new[] {"main"}, null),
                splitter.TokenizeLine("add main"),
                "Label shall be an immediate");
        }

        [TestMethod]
        public void TestBaseOffsetOperands() {
            var splitter = new SourceLineScanner();
            var expected = new ScannedSourceLine(null, "sub", new[] {"0xffe($t0)"}, null);

            Assert.AreEqual(expected, splitter.TokenizeLine("sub 0xffe($t0)"));
            Assert.AreEqual(expected, splitter.TokenizeLine("sub 0xffe ( $t0 ) "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidLine() {
            var splitter = new SourceLineScanner().TokenizeLine("67r56f");
        }
    }
}