using System;
using System.Collections.Generic;

namespace CRISP.HealthRecordsProxy.Repository.Context.ObservationContext.Models
{
    public partial class Observations
    {
        public Observations()
        {
            ObservationsCode = new HashSet<ObservationsCode>();
            ObservationsJson = new HashSet<ObservationsJson>();
            ObservationsPerformers = new HashSet<ObservationsPerformers>();
            ObservationsSpecimen = new HashSet<ObservationsSpecimen>();
        }

        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Smrn { get; set; }
        public string ParentId { get; set; }
        public string ObservationNumber { get; set; }
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Status { get; set; }
        public string AbnormalFlag { get; set; }
        public string DataAbsentReason { get; set; }
        public DateTime? DateEffective { get; set; }
        public DateTime? DateIssued { get; set; }
        public string Method { get; set; }
        public string ValueConcept { get; set; }
        public DateTime? ValueDateTime { get; set; }
        public string ValueString { get; set; }
        public string ValueQuantity { get; set; }
        public string ValueQuantityUnits { get; set; }
        public string Component { get; set; }
        public string Device { get; set; }
        public string Related { get; set; }

        public ICollection<ObservationsCode> ObservationsCode { get; set; }
        public ICollection<ObservationsJson> ObservationsJson { get; set; }
        public ICollection<ObservationsPerformers> ObservationsPerformers { get; set; }
        public ICollection<ObservationsSpecimen> ObservationsSpecimen { get; set; }
    }
}