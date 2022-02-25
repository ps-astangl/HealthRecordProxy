using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudyReason
    {
        public string Code { get; set; }
        public string System { get; set; }
        public Guid ParentId { get; set; }

        public ImagingStudy Parent { get; set; }
    }
}
