namespace Assembler.Instructions.Operands {

    /// <summary>
    ///     Marker interface representing a list of operands of some format.
    /// </summary>
    public interface IOperandList {}

    public class OperandList {

        // Properties /////////////////////////////////////////////////////////

        private string RegisterA { get; }
        private string RegisterB { get; }
        private string Immediate { get; }

    }

    public class OperandListFree {}

}