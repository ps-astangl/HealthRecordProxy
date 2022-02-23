using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.HealthRecordsProxy.Common.DomainModels;
using CRISP.HealthRecordsProxy.Common.DomainModels.Abstraction;
using CRISP.HealthRecordsProxy.Common.Mapping;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Logging;

namespace CRISP.HealthRecordProxy.Services
{
    public interface IResourceService
    {
        Task<IEnumerable<OverviewModel>> GetResources(IEnumerable<HealthRecordsRequest> healthRecordsRequest);
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

        public async Task<IEnumerable<OverviewModel>> GetResources(IEnumerable<HealthRecordsRequest> healthRecordsRequest)
        {
            var result = new List<OverviewModel>();
            foreach (var request in healthRecordsRequest)
            {
                switch (request.ResourceType)
                {
                    case nameof(Observation):
                    {
                        var observations = await _resourceClient.GetResources<ObservationReportFhirModel>(request.ResourceType, request.LogicalIdentifier.ToList());
                        var resource = observations?.Select(x => x.ToView()).Where(y => y != null)?.ToList() ?? new List<ObservationOverviewModel>();
                        if (resource.Any())
                            result.AddRange(resource);
                        break;
                    }
                    case nameof(Specimen):
                    {
                        var specimens =  await _resourceClient.GetResources<SpecimenFhirModel>(request.ResourceType, request.LogicalIdentifier.ToList());
                        var resource = specimens?.Select(x => x.ToView()).Where(y => y != null)?.ToList() ?? new List<SpecimenOverviewModel>();
                        if (resource.Any())
                            result.AddRange(resource);
                        break;
                    }
                    case nameof(ImagingStudy):
                    {
                        var imagingStudies =  await _resourceClient.GetResources<ImagingStudyFHIRModel>(request.ResourceType, request.LogicalIdentifier.ToList());
                        var resource = imagingStudies?.Select(x => x.ToView()).Where(y => y != null)?.ToList() ?? new List<ImagingStudyOverviewModel>();
                        if (resource.Any())
                            result.AddRange(resource);
                        break;
                    }
                }
            }
            return result;
        }
    }
}