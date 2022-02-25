using System;
using System.Collections.Generic;
using CRISP.HealthRecordsProxy.Common.DomainModels.Abstraction;

namespace CRISP.HealthRecordsProxy.Common.APIModels
{

    /// <summary>
    ///
    /// </summary>
    public class HealthRecordsResponse
    {
        public IEnumerable<OverviewModel> Specimens { get; set; }
        public IEnumerable<OverviewModel> Observations { get; set; }
        public IEnumerable<OverviewModel> ImagingStudy { get; set; }
    }

    // Acutal Request is an IEnumerable of this
    public class HealthRecordsRequest
    {
        public string ResourceType { get; set; }
        public IEnumerable<string> LogicalIdentifier { get; set; }
    }
    public class HealthRecordsDbRequest
    {
        public string ResourceType { get; set; }
        public IEnumerable<Guid> LogicalIdentifier { get; set; }
    }
}