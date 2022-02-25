using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudyBasedOn
    {
        public string ReferenceString { get; set; }
        public Guid ParentId { get; set; }

        public ImagingStudy Parent { get; set; }
    }
}
