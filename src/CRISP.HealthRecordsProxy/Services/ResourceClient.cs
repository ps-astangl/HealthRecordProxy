using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CRISP.Extensions.System.Base;
using CRISP.Fhir.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordProxy.Services
{
    public class ResourceClient : IResourceClient
    {

        private readonly ILogger<ResourceClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ResourceClient(ILogger<ResourceClient> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<T> GetResources<T>(string resourceName, List<string> logicalIds) where T : new()
        {
            var identifiers = logicalIds.StringJoin(',');
            var encodedIdentifier = HttpUtility.UrlEncode(identifiers);
            var bundleResult = await ExecuteFhirRequest(logicalIds, resourceName);
            return await Task.FromResult<T>(new T());
        }

        private HttpClient CreateClient(string resourceName) => _httpClientFactory.CreateClient(resourceName);

        private async Task<Bundle> ExecuteFhirRequest<TId>(IList<TId> ids, string resourceName)
        {
            var client = CreateClient(resourceName);
            var query = "?" + string.Join("&", ids.Select(id => $"_id={id}"));
            var request = new HttpRequestMessage(HttpMethod.Get, query);
            request.Headers.Add("Username", "HEALTHPROXY");
            request.Headers.Add("Orgname", "CRISP");
            var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            var bundle = JsonConvert.DeserializeObject<Bundle>(body);
            return bundle;
        }
    }


    public interface IResourceClient
    {
        public Task<T> GetResources<T>(string resourceName, List<string> logicalIds) where T : new();
    }
}