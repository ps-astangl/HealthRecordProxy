using System;
using System.Linq;
using CRISP.HealthRecordsProxy.Common.DomainModels.Abstraction;
using CRISP.Providers.Models.ImagingStudy;
using Hl7.Fhir.Model;

namespace CRISP.HealthRecordsProxy.Common.DomainModels
{
    public class ImagingStudyOverviewModel : OverviewModel
    {
        public string ViewUrl { get; }

        private const char Hashtag = '#';

        /// <summary>
        /// Constructs overview model from CRISP FHIR resources
        /// </summary>
        public ImagingStudyOverviewModel(ImagingStudyFHIRModel imagingStudy)
        {
            var endpointRef = imagingStudy.Endpoint?.FirstOrDefault(e => e.Reference.StartsWith(Hashtag))?.Reference;
            var endpointRefId = endpointRef?.TrimStart(Hashtag);
            var endpoint = imagingStudy.Contained?.FirstOrDefault(c =>
                string.Equals(c.Id, endpointRefId, StringComparison.OrdinalIgnoreCase)) as CRISP.Fhir.Models.Endpoint;

            ViewUrl = endpoint?.Address;
        }

        /// <summary>
        /// Constructs overview model from Hl7 FHIR resources
        /// </summary>
        public ImagingStudyOverviewModel(ImagingStudy imagingStudy)
        {
            var endpointRef = imagingStudy.Endpoint?.FirstOrDefault(e => e.Reference.StartsWith(Hashtag))?.Reference;
            var endpointRefId = endpointRef?.TrimStart(Hashtag);
            var endpoint = imagingStudy.Contained?.FirstOrDefault(c =>
                string.Equals(c.Id, endpointRefId, StringComparison.OrdinalIgnoreCase)) as Endpoint;

            ViewUrl = endpoint?.Address;
        }
    }
}