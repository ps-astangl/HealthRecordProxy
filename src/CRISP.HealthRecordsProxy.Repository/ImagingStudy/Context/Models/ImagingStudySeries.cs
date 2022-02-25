using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudySeries
    {
        public string Oid { get; set; }
        public Guid ParentId { get; set; }

        public ImagingStudy Parent { get; set; }
    }
}
