using System.Collections.Generic;
using System.Threading.Tasks;
using CRISP.HealthRecordProxy.Services;
using CRISP.Providers.Models.Observation;
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
    }
}