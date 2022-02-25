using System;

namespace CRISP.HealthRecordsProxy.Repository.Observations.Context.Models
{
    public partial class ObservationsSpecimen
    {
        public string SpecimenId { get; set; }
        public Guid ObservationId { get; set; }

        public Observations Observation { get; set; }
    }
}
