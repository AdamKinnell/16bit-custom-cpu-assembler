using System.Text.RegularExpressions;
using Assembler.Constants;
using Assembler.Instructions.Operands.Types;
using JetBrains.Annotations;

namespace Assembler.Parser.Operand {

    /// <summary>
    ///     Parses a base-offset type operand as specified in the source code.
    /// </summary>
    class BaseOffsetOperandParser {

        // Static Fields //////////////////////////////////////////////////////

        private static readonly Regex REGEX_BASEOFFSET
            = new Regex(RegexDefinitions.WholeInput(RegexDefinitions.BaseOffsetOperand),
                        RegexOptions.Compiled);

        // Functions //////////////////////////////////////////////////////////

        [CanBeNull]
        private RegisterOperand TryParseBase([NotNull] Match match) {
            var str = GetMatchGroupValue(match, "register");
            return str == null ? null : new RegisterOperandParser().TryParse(str);
        }

        [CanBeNull]
        private ImmediateOperand TryParseOffset([NotNull] Match match) {
            var str = GetMatchGroupValue(match, "immediate");
            return str == null ? null : new ImmediateOperandParser().TryParse(str);
        }

        /// <summary>
        ///     Get the trimmed value of a match group, if the match was successful.
        /// </summary>
        /// <param name="match"> The match object that contains the groups. </param>
        /// <param name="group"> The name of the group. </param>
        /// <returns> Null if the group was not matched. </returns>
        [CanBeNull]
        private string GetMatchGroupValue([NotNull] Match match, [NotNull] string group)
            => match.Groups[group].Success ? match.Groups[group].Value.Trim() : null;

        /// <summary>
        ///     Attempt to parse the given string as a base-offset operand.
        /// </summary>
        /// <returns> Null if no valid base-offset specified. </returns>
        [CanBeNull]
        public BaseOffsetOperand TryParse([NotNull] string baseoffset) {
            var match = REGEX_BASEOFFSET.Match(baseoffset);
            if (!match.Success) return null;

            var @base = TryParseBase(match);
            if (@base == null) return null;
            var @offset = TryParseOffset(match);
            if (@offset == null) return null;
            return new BaseOffsetOperand(@base, @offset);
        }
    }
}