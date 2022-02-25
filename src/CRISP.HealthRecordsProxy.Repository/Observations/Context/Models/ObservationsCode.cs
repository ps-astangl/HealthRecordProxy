using System;

namespace CRISP.HealthRecordsProxy.Repository.Observations.Context.Models
{
    public partial class ObservationsCode
    {
        public string LoincCode { get; set; }
        public Guid ObservationId { get; set; }

        public Observations Observation { get; set; }
    }
}