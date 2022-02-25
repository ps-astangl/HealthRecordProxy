using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudyEndpoint
    {
        public string ReferenceString { get; set; }
        public Guid ParentId { get; set; }
        public string IdentifierValue { get; set; }

        public ImagingStudy Parent { get; set; }
    }
}
