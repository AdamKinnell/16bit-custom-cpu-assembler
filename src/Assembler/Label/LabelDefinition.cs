using JetBrains.Annotations;

namespace Assembler.Label {

    /// <summary>
    ///     Represents a definition of a named address.
    /// </summary>
    public class LabelDefinition {

        // Constructors ///////////////////////////////////////////////////////

        public LabelDefinition([NotNull] string name) {
            Name = name;
            Address = null;
        }

        // Fields /////////////////////////////////////////////////////////////

        [NotNull] public string Name { get; }

        [CanBeNull] public string Address { get; }

        // Implemented Functions //////////////////////////////////////////////

        /// <inheritdoc />
        public override bool Equals(object obj)
            => obj is LabelDefinition && Equals((LabelDefinition) obj);

        /// <inheritdoc />
        protected bool Equals([NotNull] LabelDefinition other)
            => Equals(Name, other.Name);

        /// <inheritdoc />
        public override int GetHashCode() 
            => Name.GetHashCode();
    }
}