using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CRISP.Extensions.System.Base;
using CRISP.Fhir.Models;
using CRISP.Providers.Models.Observation;

namespace CRISP.HealthRecordsProxy.Common.Extensions
{
    /// <summary>
    /// Stolen from InContext -- It hurts to look at, assume it works
    /// </summary>
    public static class PackageExtensions
    {
        private static readonly Regex UiStringRegex = new Regex(@"\s\s+");
        private const string SingleWhiteSpace = " ";

        public static string ToUiString(this CodeableConcept codeableConcept)
        {
            if (!string.IsNullOrWhiteSpace(codeableConcept.Text))
            {
                return codeableConcept.Text;
            }

            var output = codeableConcept.Coding?.Select(coding => $"{coding.Code} {coding.Display}".Trim())
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .StringJoin(", ");

            return !string.IsNullOrWhiteSpace(output) ? output : string.Empty;
        }


        public static string ToUiString(this Quantity quantity) =>
            $"{quantity.Comparator?.Trim()}{FormatRationalNumber(quantity.Value)}{FormatUnits(quantity.Unit)}";


        private static string FormatRationalNumber(IFormattable val)
        {
            const int minimumDecimalDigits = 2;

            if (string.IsNullOrWhiteSpace(val?.ToString()))
                return string.Empty;

            var decimalPart = val
                .ToString("G", CultureInfo.InvariantCulture)
                .Split(".")
                .ElementAtOrDefault(1);

            if (decimalPart is null || decimalPart.Length <= minimumDecimalDigits)
                return val.ToString("F", CultureInfo.InvariantCulture);

            return val.ToString("G", CultureInfo.InvariantCulture);
        }

        private static string FormatUnits(string units) =>
            !string.IsNullOrWhiteSpace(units) ? $" {units}" : string.Empty;

        public static string ToUiString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            return UiStringRegex.Replace(str, SingleWhiteSpace)
                .Replace("\r", SingleWhiteSpace)
                .Replace("\n", SingleWhiteSpace)
                .Trim();
        }

        public static string ToUiString(this FHIRDateTime fhirDateTime) => fhirDateTime.ToString();

        public static string ToUiString(this ReferenceRange referenceRange)
        {
            var text = referenceRange.Text;

            return !string.IsNullOrWhiteSpace(text)
                ? text
                : QuantityRangeBuilder(referenceRange.Low, referenceRange.High, " - ", false);
        }

        private static string QuantityRangeBuilder(SimpleQuantity low, SimpleQuantity high, string separator,
            bool includeUnits = true)
        {
            var stringBuilder = new StringBuilder();
            var lowValue = FormatRationalNumber(low?.Value);
            var lowUnits = low?.Unit;
            var highValue = FormatRationalNumber(high?.Value);
            var highUnits = high?.Unit;

            if (string.IsNullOrWhiteSpace(lowValue) && string.IsNullOrWhiteSpace(highValue))
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(highValue) && !string.IsNullOrWhiteSpace(lowValue))
            {
                stringBuilder.AppendJoin(separator, lowValue, highValue);
            }
            else if (!string.IsNullOrWhiteSpace(lowValue))
            {
                stringBuilder.Append(">");
                stringBuilder.Append(lowValue);
                if (includeUnits)
                {
                    stringBuilder.Append(FormatUnits(lowUnits));
                }
            }
            else
            {
                stringBuilder.Append("<");
                stringBuilder.Append(highValue);
                if (includeUnits)
                {
                    stringBuilder.Append(FormatUnits(highUnits));
                }
            }

            return stringBuilder.ToString();
        }

        public static string ToUiString(this bool value) => value.ToString();
        public static string ToUiString(this Attachment attachment) => attachment.Data;

        public static string ToUiString(this Period period)
        {
            var start = period.Start;
            var end = period.End;
            string output;

            if (start == null && end == null)
            {
                output = string.Empty;
            }
            else if (start != null && end != null)
            {
                output = $"{start.Value.ToString("G")} - {end.Value.ToString("G")}";
            }
            else if (start != null)
            {
                output = $"> {start.Value.ToString("G")}";
            }
            else
            {
                output = $"< {end.Value.ToString("G")}";
            }

            return output;
        }

        public static string ToUiString(this CRISP.Fhir.Models.Range range) =>
            QuantityRangeBuilder(range.Low, range.High, " - ");

        public static string ToUiString(this SampledData sampledData) => sampledData.Data;

        public static string ToUiString(this Ratio ratio)
        {
            var numerator = ratio.Numerator;
            var denominator = ratio.Denominator;

            // validation ensures that either both numerator and denominator are present or neither are present
            return numerator == null && denominator == null
                ? string.Empty
                : string.Join("/",
                    BuildRatioComponent(numerator.Comparator ?? denominator.Comparator, numerator.Value ?? 1,
                        numerator.Unit),
                    BuildRatioComponent(null, denominator.Value ?? 1, denominator.Unit));
        }

        private static string BuildRatioComponent(string comparator, double value, string units)
        {
            var emptyComparatorIfNeeded = !string.IsNullOrEmpty(comparator) ? comparator : string.Empty;

            return
                $"{emptyComparatorIfNeeded}{FormatRationalNumber(value)}{FormatUnits(units)}";
        }
    }
}