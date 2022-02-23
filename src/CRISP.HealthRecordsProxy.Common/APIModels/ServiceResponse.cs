using System.Collections.Generic;
using CRISP.HealthRecordsProxy.Common.DomainModels;

namespace CRISP.HealthRecordsProxy.Common.APIModels
{

    /// <summary>
    ///
    /// </summary>
    public class HealthRecordsResponse
    {
        public IList<SpecimenOverviewModel> Specimens { get; }
        public IList<ObservationOverviewModel> Observations { get; }
        public string ImagingStudyViewUrl { get; set; }
    }

    // Acutal Request is an IEnumerable of this
    public class HealthRecordsRequest
    {
        public string ResourceType { get; set; }
        public IEnumerable<string> LogicalIdentifier { get; set; }
    }
}