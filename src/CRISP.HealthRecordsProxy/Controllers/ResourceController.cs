using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordProxy.Services;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.HealthRecordsProxy.Common.DomainModels;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordProxy.Controllers
{
    /// <summary>
    /// Main Controller for getting resources
    /// </summary>
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        private readonly IResourceService _resourceService;

        /// <inheritdoc />
        public ResourceController(ILogger<ResourceController> logger, IResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetAllExample()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            HealthRecordsRequest observationRequest = ExampleRequests.ObservationHealthRecordsRequest();
            HealthRecordsRequest specimenRequest = ExampleRequests.SpecimenHealthRecordsRequest();
            HealthRecordsRequest imagingStudyRequest = ExampleRequests.ImagingStudyHealthRecordsRequest();
            healthRecordsRequests.Add(observationRequest);
            healthRecordsRequests.Add(specimenRequest);
            healthRecordsRequests.Add(imagingStudyRequest);

            return await CallForResources(healthRecordsRequests);
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetObservationExample()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            var healthRecordsRequest = ExampleRequests.ObservationHealthRecordsRequest();
            healthRecordsRequests.Add(healthRecordsRequest);
            return await CallForResources(healthRecordsRequests);
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetSpecimenExample()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            var healthRecordsRequest = ExampleRequests.SpecimenHealthRecordsRequest();
            healthRecordsRequests.Add(healthRecordsRequest);
            return await CallForResources(healthRecordsRequests);
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetImagingStudyExample()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            var healthRecordsRequest = ExampleRequests.ImagingStudyHealthRecordsRequest();
            healthRecordsRequests.Add(healthRecordsRequest);
            return await CallForResources(healthRecordsRequests);
        }

        public async Task<dynamic> CallForResources([FromBody] IEnumerable<HealthRecordsRequest> healthRecordsRequest)
        {
            var resources = await _resourceService.GetResources(healthRecordsRequest);
            var observations = resources.Where(x => x is ObservationOverviewModel);
            var specimens = resources.Where(x => x is SpecimenOverviewModel);
            var imagingstudy = resources.Where(x => (x is ImagingStudyOverviewModel));

            HealthRecordsResponse healthRecordsResponse = new HealthRecordsResponse
            {
                Specimens = specimens,
                Observations = observations,
                ImagingStudy = imagingstudy
            };
            var stringResult = JsonConvert.SerializeObject(healthRecordsResponse);
            return new ContentResult
            {
                ContentType = "Application/json",
                Content = stringResult,
                StatusCode = 200
            };
        }

        [HttpGet, Route("[action]")]
        public dynamic GetExampleRequest()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            HealthRecordsRequest observationRequest = ExampleRequests.ObservationHealthRecordsRequest();
            HealthRecordsRequest specimenRequest = ExampleRequests.SpecimenHealthRecordsRequest();
            HealthRecordsRequest imagingStudyRequest = ExampleRequests.ImagingStudyHealthRecordsRequest();
            healthRecordsRequests.Add(observationRequest);
            healthRecordsRequests.Add(specimenRequest);
            healthRecordsRequests.Add(imagingStudyRequest);
            return healthRecordsRequests;
        }
    }
}