using System.Diagnostics.CodeAnalysis;
using Assembler.Splitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Splitter {
    [TestClass]
    public class SourceLineSplitterTests {

        [TestMethod]
        public void TestEmptyLine() {
            var splitter = new SourceLineSplitter();
            Assert.IsTrue(splitter.SplitLine("").IsEmpty);
            Assert.IsTrue(splitter.SplitLine("    ").IsEmpty);
        }

        [TestMethod]
        public void TestSinglePartLine() {
            var splitter = new SourceLineSplitter();

            // Label Only
            Assert.AreEqual(
                new SourceLine(
                    label: "main",
                    mnemonic: null,
                    operands: null,
                    comment: null
                ),
                splitter.SplitLine("main:"));

            // Mnemonic Only
            Assert.AreEqual(
                new SourceLine(
                    label: null,
                    mnemonic: "add",
                    operands: null,
                    comment: null
                ),
                splitter.SplitLine("add"));

            // Comment Only
            Assert.AreEqual(
                new SourceLine(
                    label: null,
                    mnemonic: null,
                    operands: null,
                    comment: "This is a comment"
                ),
                splitter.SplitLine("# This is a comment"));
        }

        [TestMethod]
        public void TestFullLine() {
            var splitter = new SourceLineSplitter();
            var expected = new SourceLine(
                label: "_MaiN_",
                mnemonic: "AnD",
                operands: "$t0 $t1 3260",
                comment: ":$#$:comment:$#$:"
            );
            Assert.AreEqual(
                expected,
                splitter.SplitLine("_MaiN_: AnD $t0, $t1, 3260 #:$#$:comment:$#$:"));
            Assert.AreEqual(
                expected,
                splitter.SplitLine("_MaiN_:AnD $t0,$t1,3260#:$#$:comment:$#$:"));
            Assert.AreEqual(
                expected,
                splitter.SplitLine("  _MaiN_  :  AnD  $t0  ,  $t1  ,  3260  #  :$#$:comment:$#$:  "));
        }

        [TestMethod]
        public void TestInstructionCleanup() {
            var splitter = new SourceLineSplitter();
            var expected = new SourceLine(
                label: null,
                mnemonic: "and",
                operands: "$t0 $t1",
                comment: null
            );
            Assert.AreEqual(expected, splitter.SplitLine("and $t0 $t1"));
            Assert.AreEqual(expected, splitter.SplitLine("and $t0  $t1"));
            Assert.AreEqual(expected, splitter.SplitLine("and $t0, $t1"));
            Assert.AreEqual(expected, splitter.SplitLine("and $t0 , $t1"));
            Assert.AreEqual(expected, splitter.SplitLine("and $t0 ,$t1"));
            Assert.AreEqual(expected, splitter.SplitLine("and $t0,$t1"));
        }

        [TestMethod]
        public void TestImmediateOperands() {
            var splitter = new SourceLineSplitter();

            // Decimal
            Assert.AreEqual(
                new SourceLine(null, "add", "100", null),
                splitter.SplitLine("add 100"),
                "Decimal shall be an immediate");

            // Hexadecimal
            Assert.AreEqual(
                new SourceLine(null, "add", "0x019ABCF", null),
                splitter.SplitLine("add 0x019ABCF"),
                "Hexadecimal shall be an immediate");

            // Binary
            Assert.AreEqual(
                new SourceLine(null, "add", "0b10010101", null),
                splitter.SplitLine("add 0b10010101"),
                "Binary shall be an immediate");

            // Label
            Assert.AreEqual(
                new SourceLine(null, "add", "main", null),
                splitter.SplitLine("add main"),
                "Label shall be an immediate");
        }

        [TestMethod]
        public void TestBaseOffsetOperands() {
            var splitter = new SourceLineSplitter();

            Assert.AreEqual(
                new SourceLine(null, "sub", "0xffe($t0)", null),
                splitter.SplitLine("sub 0xffe($t0)"));
            Assert.AreEqual(
                new SourceLine(null, "sub", "0xffe ( $t0 )", null),
                splitter.SplitLine("sub 0xffe ( $t0 ) "));
        }
    }
}