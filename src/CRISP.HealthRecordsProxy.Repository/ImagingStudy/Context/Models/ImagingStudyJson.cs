using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudyJson
    {
        public int Id { get; set; }
        public Guid ParentId { get; set; }
        public string Response { get; set; }

        public ImagingStudy Parent { get; set; }
    }
}
