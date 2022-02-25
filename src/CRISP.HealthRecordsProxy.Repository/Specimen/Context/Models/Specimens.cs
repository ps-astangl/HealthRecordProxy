using System;
using System.Collections.Generic;

namespace CRISP.HealthRecordsProxy.Repository.Specimen.Context.Models
{
    public partial class Specimens
    {
        public Specimens()
        {
            ParentSpecimensSpecimen = new HashSet<ParentSpecimensSpecimen>();
            SpecimensJson = new HashSet<SpecimensJson>();
        }

        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Smrn { get; set; }
        public string ParentId { get; set; }
        public string SpecimenNumber { get; set; }
        public string AccessionIdentifier { get; set; }
        public string BodySite { get; set; }
        public DateTime? CollectedDateTime { get; set; }
        public string Collector { get; set; }
        public string Container { get; set; }
        public string Status { get; set; }
        public string SpecimenType { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<ParentSpecimensSpecimen> ParentSpecimensSpecimen { get; set; }
        public ICollection<SpecimensJson> SpecimensJson { get; set; }
    }
}
