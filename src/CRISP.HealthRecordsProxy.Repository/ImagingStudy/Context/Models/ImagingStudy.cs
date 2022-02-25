using System;
using System.Collections.Generic;

namespace CRISP.HealthRecordsProxy.Repository.Context.Models
{
    public partial class ImagingStudy
    {
        public ImagingStudy()
        {
            ImagingStudyBasedOn = new HashSet<ImagingStudyBasedOn>();
            ImagingStudyBodySite = new HashSet<ImagingStudyBodySite>();
            ImagingStudyDicomClass = new HashSet<ImagingStudyDicomClass>();
            ImagingStudyEndpoint = new HashSet<ImagingStudyEndpoint>();
            ImagingStudyIdentifier = new HashSet<ImagingStudyIdentifier>();
            ImagingStudyJson = new HashSet<ImagingStudyJson>();
            ImagingStudyModality = new HashSet<ImagingStudyModality>();
            ImagingStudyPerformer = new HashSet<ImagingStudyPerformer>();
            ImagingStudyReason = new HashSet<ImagingStudyReason>();
            ImagingStudySeries = new HashSet<ImagingStudySeries>();
            ImagingStudyUid = new HashSet<ImagingStudyUid>();
        }

        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Smrn { get; set; }
        public string FullId { get; set; }
        public string Accession { get; set; }
        public string Context { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateStarted { get; set; }
        public string Study { get; set; }
        public DateTime? StudyDate { get; set; }

        public ICollection<ImagingStudyBasedOn> ImagingStudyBasedOn { get; set; }
        public ICollection<ImagingStudyBodySite> ImagingStudyBodySite { get; set; }
        public ICollection<ImagingStudyDicomClass> ImagingStudyDicomClass { get; set; }
        public ICollection<ImagingStudyEndpoint> ImagingStudyEndpoint { get; set; }
        public ICollection<ImagingStudyIdentifier> ImagingStudyIdentifier { get; set; }
        public ICollection<ImagingStudyJson> ImagingStudyJson { get; set; }
        public ICollection<ImagingStudyModality> ImagingStudyModality { get; set; }
        public ICollection<ImagingStudyPerformer> ImagingStudyPerformer { get; set; }
        public ICollection<ImagingStudyReason> ImagingStudyReason { get; set; }
        public ICollection<ImagingStudySeries> ImagingStudySeries { get; set; }
        public ICollection<ImagingStudyUid> ImagingStudyUid { get; set; }
    }
}
