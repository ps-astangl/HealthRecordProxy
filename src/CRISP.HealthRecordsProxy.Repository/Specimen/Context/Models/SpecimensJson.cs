using System;

namespace CRISP.HealthRecordsProxy.Repository.Specimen.Context.Models
{
    public partial class SpecimensJson
    {
        public int Id { get; set; }
        public Guid SpecimenId { get; set; }
        public string Response { get; set; }

        public Specimens Specimen { get; set; }
    }
}
