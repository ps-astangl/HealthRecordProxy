using System.Linq;
using CRISP.Extensions.System.Base;
using CRISP.HealthRecordsProxy.Common.DomainModels.Abstraction;
using CRISP.HealthRecordsProxy.Common.Extensions;
using CRISP.Providers.Models.Observation;

namespace CRISP.HealthRecordsProxy.Common.DomainModels
{

    public class ObservationOverviewModel : OverviewModel
    {
        public ObservationOverviewModel()
        {
        }

        public string Name { get; set; }
        public string Result { get; set; }
        public string Range { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Interpretation { get; set; }
        public string Issued { get; set; }

        /// <summary>
        /// Constructs overview model from CRISP FHIR resources
        /// </summary>
        public ObservationOverviewModel(ObservationReportFhirModel observation)
        {
            Name = observation.Code?.ToUiString() ?? string.Empty;
            Result = GetResult(observation);
            Range = GetRangeAsString(observation);
            Comment = observation?.Comment?.ToUiString() ?? string.Empty;
            Status = observation?.Status?.Trim() ?? string.Empty;
            Interpretation = observation?.Interpretation?.Text ?? string.Empty;
            Issued = observation?.Issued?.ToUiString() ?? string.Empty;
        }

        private static string GetRangeAsString(ObservationReportFhirModel observation)
        {
            return observation?.ReferenceRange
                ?.Select(rr => rr?.ToUiString())?.StringJoin(", ") ?? string.Empty;
        }

        private static string GetResult(ObservationReportFhirModel observation)
        {
            // CRISP Observation has separate members for each possible value type, rather than one member with a
            // base type, like the Hl7 library. As such, the pattern matching approach below is impossible here.
            return observation.ValueString?.ToUiString()
                   ?? observation.ValueTime
                   ?? observation.ValueBoolean?.ToUiString()
                   ?? observation.ValueAttachment?.ToUiString()
                   ?? observation.ValuePeriod?.ToUiString()
                   ?? observation.ValueQuantity?.ToUiString()
                   ?? observation.ValueRange?.ToUiString()
                   ?? observation.ValueRatio?.ToUiString()
                   ?? observation.ValueCodeableConcept?.ToUiString()
                   ?? observation.ValueDateTime?.ToUiString()
                   ?? observation.ValueSampledData?.ToUiString()
                   ?? string.Empty;
        }
    }
}