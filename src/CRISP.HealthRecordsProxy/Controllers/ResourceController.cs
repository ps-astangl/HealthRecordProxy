using System.Collections.Generic;
using System.Threading.Tasks;
using CRISP.HealthRecordProxy.Services;
using CRISP.HealthRecordsProxy.Common.DomainModels;
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
                .GetResources<ObservationOverviewModel>("Observation",
                    new List<string> {"b588f32c-b9c8-eb6f-a0db-6a167808bbd1"});
            return Ok(resources);
        }
    }
}