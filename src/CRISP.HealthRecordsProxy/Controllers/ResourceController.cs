using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordProxy.Services;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.Providers.Models.Observation;
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

        [HttpPost]
        public async Task<ActionResult> CallForResources(
            [FromBody] IEnumerable<HealthRecordsRequest> healthRecordsRequest)
        {
            var resources = await _resourceService.GetResources(healthRecordsRequest);
            var stringResult = JsonConvert.SerializeObject(resources);
            return new ContentResult
            {
                ContentType = "Application/json",
                Content = stringResult,
                StatusCode = 200
            };
        }

        [HttpGet, Route("[action]")]
        public async Task<ActionResult> GetObservationRequest()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
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
            healthRecordsRequests.Add(healthRecordsRequest);
            return await CallForResources(healthRecordsRequests);
        }

        [HttpGet, Route("[action]")]
        public async Task<ActionResult> GetSpecimenRequest()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(Specimen),
                LogicalIdentifier = new string[]
                {
                    "FF50D2A0-E3EE-5CBE-916E-E0F5A535CDC1",
                    "B3D86F8B-8FDD-C1F6-F7C4-3AC21CD66418",
                    "44D79F55-F0DF-5059-D6BA-F677092C1EA0",
                    "0E08FEEA-2DB7-8F02-1471-351962F3974D",
                    "9B4841DE-1EF5-8B5C-9D23-E12048572186",
                    "AEAEAE5B-4DB2-3BF6-A7CF-A84810B85612",
                    "603D3038-4AA8-D5FD-93F3-A3A020862202",
                    "5439307F-9F67-A701-651F-639FE3CF4B57",
                    "CB7125AB-C516-BDF1-22B1-80141D1815A7",
                    "E4A6A8C4-E7C1-D5FC-B231-4507BA3A193F"
                }
            };
            healthRecordsRequests.Add(healthRecordsRequest);
            return await CallForResources(healthRecordsRequests);
        }

        [HttpGet, Route("[action]")]
        public async Task<ActionResult> GetImagingStudyRequest()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(ImagingStudy),
                LogicalIdentifier = new string[] {
                "F1E7CDB4-B48C-A858-C4CD-0906A6DD7929",
                "466FAD63-FAF0-E42F-ACFA-2B1C5D9C272D",
                "D45174E0-0E58-EE54-BE84-33C855425358",
                "418C3FA6-427C-B2C5-B1A1-59F38F482C67",
                "5F495B98-C896-A848-9C57-6B862C89EF7E",
                "BAE3627B-1E5F-23F3-5E92-6DAE624DBCC0",
                "B5AC5B2E-7B40-21B8-A8F8-7A310DF04A2D",
                "CF841EBA-38C9-0E85-EEBA-889935675298",
                "6DA1A00D-6D7F-55F2-A0A2-CE70392EF068",
                }
            };
            healthRecordsRequests.Add(healthRecordsRequest);
            return await CallForResources(healthRecordsRequests);
        }
    }
}