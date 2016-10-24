using System;
using Assembler.Instructions;
using Assembler.Instructions.Definitions;
using Assembler.Operands;
using Assembler.Operands.Types;
using Assembler.Registries;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembler.Tests.Registries {

    [TestClass]
    public class InstructionRegistryTests {

        // Helper Functions ///////////////////////////////////////////////////

        [NotNull]
        private InstructionFieldMappingBuilder.InstructionFieldMapping GenerateMockFieldMapping()
            => new InstructionFieldMappingBuilder()
                .Opcode(0)
                .Function(0)
                .R1(0)
                .R2(0)
                .Immediate(0)
                .Build();

        [NotNull]
        private InstructionFormat GenerateInstructionFormat()
            => new InstructionFormat(
                "test", new OperandFormat(
                    typeof(RegisterOperand),
                    typeof(ImmediateOperand),
                    typeof(BaseOffsetOperand))
            );

        [NotNull]
        private NativeInstructionDefinition GenerateNativeInstruction()
            => new NativeInstructionDefinition(
                GenerateInstructionFormat(),
                GenerateMockFieldMapping()
            );

        // Test Functions /////////////////////////////////////////////////////

        [TestMethod]
        public void GivenUnregisteredNativeInstruction_Register_Succeeds()
            => new InstructionRegistry().Register(GenerateNativeInstruction());

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenRegisteredNativeInstruction_Register_Throws() {
            var registry = new InstructionRegistry();
            registry.Register(GenerateNativeInstruction());
            registry.Register(GenerateNativeInstruction());
        }

        [TestMethod]
        public void GivenUnregisteredInstructionFormat_IsRegistered_ReturnsFalse()
            => Assert.IsFalse(new InstructionRegistry().IsRegistered(GenerateInstructionFormat()));

        [TestMethod]
        public void GivenUnregisteredInstructionFormat_Find_ReturnsNull()
            => Assert.IsNull(new InstructionRegistry().Find(GenerateInstructionFormat()));

        [TestMethod]
        public void GivenReferenceEqualRegisteredInstructionFormat_IsRegistered_ReturnsTrue() {
            var registry = new InstructionRegistry();
            var instruction = GenerateNativeInstruction();
            registry.Register(instruction);

            // Same format object as original instruction.
            Assert.IsTrue(registry.IsRegistered(instruction.Format));
        }

        [TestMethod]
        public void GivenValueEqualRegisteredInstructionFormat_IsRegistered_ReturnsTrue() {
            var registry = new InstructionRegistry();
            var instruction = GenerateNativeInstruction();
            registry.Register(instruction);

            // Different format object from original instruction.
            Assert.IsTrue(registry.IsRegistered(GenerateInstructionFormat()));
        }

        [TestMethod]
        public void GivenReferenceEqualRegisteredInstructionFormat_Find_ReturnsOriginalRegisteredInstruction() {
            var registry = new InstructionRegistry();
            var instruction = GenerateNativeInstruction();
            registry.Register(instruction);

            // Same format object as original instruction.
            Assert.IsNotNull(registry.Find(instruction.Format));
        }

        [TestMethod]
        public void GivenValueEqualRegisteredInstructionFormat_Find_ReturnsOriginalRegisteredInstruction() {
            var registry = new InstructionRegistry();
            var instruction = GenerateNativeInstruction();
            registry.Register(instruction);

            // Different format object from original instruction.
            Assert.IsNotNull(registry.Find(GenerateInstructionFormat()));
        }
    }
}