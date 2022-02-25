using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudyDicomClass
    {
        public string Oid { get; set; }
        public Guid ParentId { get; set; }

        public ImagingStudy Parent { get; set; }
    }
}
