using System.Collections.Generic;

namespace CRISP.HealthRecordsProxy.Common.DomainModels
{

    /// <summary>
    ///
    /// </summary>
    public class ServiceResponse
    {
        public IList<SpecimenOverviewModel> Specimens { get; }
        public IList<ObservationOverviewModel> Observations { get; }
        public string ImagingStudyViewUrl { get; set; }
    }

    public class ServiceRequest
    {
        private string ResourceType { get; set; }
        private IEnumerable<string> logicalIds { get; set; }
    }
}