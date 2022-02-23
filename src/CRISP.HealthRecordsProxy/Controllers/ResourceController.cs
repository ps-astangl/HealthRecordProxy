using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordProxy.Services;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.Providers.Models.Observation;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRISP.HealthRecordProxy.Controllers
{
    /// <summary>
    /// Main Controller for getting resources
    /// </summary>
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        private readonly IResourceClient _resourceClient;

        /// <inheritdoc />
        public ResourceController(ILogger<ResourceController> logger, IResourceClient resourceClient)
        {
            _logger = logger;
            _resourceClient = resourceClient;
        }

        [HttpGet, Route("[action]")]
        public async Task<IActionResult> Test()
        {
            var resources = await _resourceClient
                .GetResources<ObservationReportFhirModel>("Observation",
                    new List<string> {"BAC0482A-379D-5CB3-E24F-5BF930324840"});
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> CallForResources([FromBody] IEnumerable<HealthRecordsRequest> healthRecordsRequest)
        {
            var resources = await _resourceClient
                .GetResources<ObservationReportFhirModel>("Observation",
                    new List<string> {"BAC0482A-379D-5CB3-E24F-5BF930324840"});
            return Ok(resources);
        }

        [HttpGet, Route("[action]")]
        public ActionResult GetExampleRequest()
        {
            IEnumerable<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(Observation),
                LogicalIdentifier = new string[]
                {
                    "21748C07-AA58-A460-8518-1A93F48F424F",
                    "C1258861-497D-D89B-C833-17E5CBB246F5",
                    "73B70122-B0F9-5EF9-C55F-9855D0F0AB99",
                    "7F509AFB-6E6A-E11F-6250-4AAEAB51A1BF",
                    "5C0D29A3-7D67-7441-8901-CEC65AFAD2AE",
                    "D2112447-4D52-AD41-7D6A-A9AB63CE5EA9",
                    "54F4B253-C2E2-EF81-AFDA-2A9919411E59",
                    "429FFBD4-0059-3DAD-468A-ABB56C82FF85",
                    "692537FB-C2E8-1FE1-BCB5-BC81A80E18A6",
                    "F20005DB-3C5D-817A-6AFE-3DE73488EE9E"
                }
            };
            return Ok(healthRecordsRequests.Append(healthRecordsRequest));
        }
    }
}