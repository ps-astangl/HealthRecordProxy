using System;

namespace CRISP.HealthRecordsProxy.Repository.Observations.Context.Models
{
    public partial class ObservationsJson
    {
        public int Id { get; set; }
        public Guid ObservationId { get; set; }
        public string Response { get; set; }

        public Observations Observation { get; set; }
    }
}
