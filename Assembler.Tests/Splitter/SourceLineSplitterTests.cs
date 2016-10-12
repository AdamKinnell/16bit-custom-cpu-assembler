using System.Diagnostics.CodeAnalysis;
using Assembler.Splitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Splitter {
    [TestClass]
    public class SourceLineSplitterTests {

        [TestMethod]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
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
        }
    }
}