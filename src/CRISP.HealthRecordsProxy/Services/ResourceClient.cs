using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CRISP.Fhir.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordProxy.Services
{
    public interface IResourceClient
    {
        public Task<IEnumerable<T>> GetResources<T>(string resourceName, List<Guid> logicalIds);
    }

    public class ResourceClient : IResourceClient
    {
        private readonly ILogger<ResourceClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string SystemUserName = "HealthRecordProxy";
        private const string SystemOrganization = "CRISP";

        public ResourceClient(ILogger<ResourceClient> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetResources<T>(string resourceName, List<Guid> logicalIds)
        {
            return await ExecuteFhirRequest<T>(logicalIds, resourceName);
        }

        private HttpClient CreateClient(string resourceName) => _httpClientFactory.CreateClient(resourceName);

        private async Task<IEnumerable<TOut>> ExecuteFhirRequest<TOut>(IList<Guid> ids, string resourceName)
        {
            var client = CreateClient(resourceName);
            var query = "?" + string.Join("&", ids.Select(id => $"_id={id}"));
            var request = new HttpRequestMessage(HttpMethod.Get, query);

            // Set the headers for the request
            request.Headers.Add("Username", SystemUserName);
            request.Headers.Add("Orgname", SystemOrganization);

            try
            {
                var response = await client.SendAsync(request);
                var isBundle = TryHandleBundle(response, out Bundle bundle);
                if (isBundle)
                    return bundle.Entry.Where(x => x.BaseEntryResource is TOut)
                        .Select(x => x.BaseEntryResource)
                        .Cast<TOut>();

                return null;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An exception has occured while attempting to obtain the FHIR Resource");
                throw;
            }
        }

        private bool TryHandleBundle(HttpResponseMessage message, out Bundle bundle)
        {
            if (!message.IsSuccessStatusCode)
            {
                _logger.LogInformation(":: Bundle request failed with status code {StatusCode}", message.StatusCode);
                bundle = null;
                return false;
            }

            var body = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // Check if body present
            if (string.IsNullOrWhiteSpace(body))
            {
                _logger.LogInformation(":: Bundle request is empty");
                bundle = null;
                return false;
            }

            try
            {
                bundle = JsonConvert.DeserializeObject<Bundle>(body);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Bundle Failed to Deserialize");
                bundle = null;
                return false;
            }


            return true;
        }
    }
}