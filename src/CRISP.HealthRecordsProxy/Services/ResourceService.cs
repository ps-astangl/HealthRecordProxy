using System.Collections.Generic;
using System.Linq;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Logging;

namespace CRISP.HealthRecordProxy.Services
{
    public interface IResourceService
    {

    }
    public class ResourceService : IResourceService
    {
        private readonly ILogger<ResourceService> _logger;
        private readonly IResourceClient _resourceClient;

        public ResourceService(ILogger<ResourceService> logger, IResourceClient resourceClient)
        {
            _logger = logger;
            _resourceClient = resourceClient;
        }

        public async System.Threading.Tasks.Task GetResources(IEnumerable<HealthRecordsRequest> healthRecordsRequest)
        {
            foreach (var request in healthRecordsRequest)
            {
                switch (request.ResourceType)
                {
                    case nameof(Observation):
                    {
                        var observations = await _resourceClient.GetResources<ObservationReportFhirModel>(request.ResourceType, request.LogicalIdentifier.ToList());
                        return;
                    }
                    case nameof(Specimen):
                    {
                        var specimens =  await _resourceClient.GetResources<SpecimenFhirModel>(request.ResourceType, request.LogicalIdentifier.ToList());
                        return;
                    }
                    case nameof(ImagingStudy):
                    {
                        if (request.ResourceType == nameof(ImagingStudy))
                        {
                            var imagingStudies =  await _resourceClient.GetResources<ImagingStudyFHIRModel>(request.ResourceType, request.LogicalIdentifier.ToList());
                        }

                        break;
                    }
                }
            }
        }
    }
}