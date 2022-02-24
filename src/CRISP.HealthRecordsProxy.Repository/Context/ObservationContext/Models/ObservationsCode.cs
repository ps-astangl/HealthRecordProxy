using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.ObservationContext.Models
{
    public partial class ObservationsCode
    {
        public string LoincCode { get; set; }
        public Guid ObservationId { get; set; }

        public Observations Observation { get; set; }
    }
}