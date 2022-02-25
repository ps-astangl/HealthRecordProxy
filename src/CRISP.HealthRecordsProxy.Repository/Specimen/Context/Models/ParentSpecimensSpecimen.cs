using System;

namespace CRISP.HealthRecordsProxy.Repository.Specimen.Context.Models
{
    public partial class ParentSpecimensSpecimen
    {
        public string ParentSpecimenId { get; set; }
        public Guid SpecimenId { get; set; }

        public Specimens Specimen { get; set; }
    }
}
