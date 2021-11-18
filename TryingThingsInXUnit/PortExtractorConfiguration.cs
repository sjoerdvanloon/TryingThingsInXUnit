namespace TryingThingsInXUnit
{
    public class PortExtractorConfiguration
    {
        public char PartSeparator { get; set; } = ',';
        public char RangeIndicator { get; set; } = '-';
        public char MaskIndicator { get; set; } = '*';
        public bool EnableRanges { get; set; } = true;
        public bool EnableMasking { get; set; } = true;

        public struct ValidationResult { public bool Valid { get => string.IsNullOrWhiteSpace(Reason); } public string Reason { get; set; } };
        public ValidationResult Validate()
        {

            if (EnableRanges && PartSeparator == RangeIndicator)
                return new ValidationResult() { Reason = $"{nameof(PartSeparator)} and {nameof(RangeIndicator)} are both {PartSeparator}" };

            if (EnableMasking && PartSeparator == MaskIndicator)
                return new ValidationResult() { Reason = $"{nameof(PartSeparator)} and {nameof(MaskIndicator)} are both {PartSeparator}" };

            if (EnableMasking && EnableRanges && RangeIndicator == MaskIndicator)
                return new ValidationResult() { Reason = $"{nameof(MaskIndicator)} and {nameof(RangeIndicator)} are both {MaskIndicator}" };

            return new ValidationResult() { };
        }
    }
}
