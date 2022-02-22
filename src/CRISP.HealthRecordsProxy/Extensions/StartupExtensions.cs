using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using CRISP.Extensions.DependencyInjection;
using CRISP.HealthRecordProxy.Configurations;
using CRISP.HealthRecordProxy.Services;
using CRISP.HealthRecordsProxy.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CRISP.HealthRecordProxy.Extensions
{

    public static class StartupExtensions
    {
        public static void AddResourceService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddTransient<IResourceClient, ResourceClient>();
            serviceCollection.AddObservationClient(configuration);
        }

        public static IServiceCollection AddObservationClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            const string observationClientConfiguration = "ObservationClientConfiguration";
            const string clientName = "Observation";

            // add IHttpClientFactory for the web client
            var config = configuration
                .GetSection(observationClientConfiguration)
                .Get<ClientConfiguration>();

            var httpClientConfig = new HttpClientConfiguration(
                clientName: clientName,
                baseAddress: new Uri(config.BaseAddress),
                timeout: TimeSpan.FromSeconds(config.TimeoutSeconds));

            services
                .AddNamedHttpClient(httpClientConfig)
                .AddClientErrorPolicy(config);

            return services;
        }

        public static IHttpClientBuilder AddNamedHttpClient(
            this IServiceCollection serviceCollection,
            HttpClientConfiguration configuration)
        {
            X509Certificate2 cert = configuration != null
                ? configuration.X509Certificate2
                : throw new ArgumentNullException(nameof(configuration));
            return HttpClientFactoryServiceCollectionExtensions.AddHttpClient(serviceCollection,
                    configuration.ClientName,
                    (Action<HttpClient>) (httpClient => SetHttpClientProperties(httpClient, configuration)))
                .ConfigurePrimaryHttpMessageHandler((Func<HttpMessageHandler>) (() =>
                    (HttpMessageHandler) CreateDefaultHttpClientHandler((X509Certificate) cert)));
        }

        private static void AddClientErrorPolicy(this IHttpClientBuilder httpClientBuilder,
            ClientConfiguration clientConfiguration)
        {
            httpClientBuilder
                .SetHandlerLifetime(TimeSpan.FromMinutes(clientConfiguration.RetryHandlerLifeTimeInMinutes))
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                    .WaitAndRetryAsync(clientConfiguration.RetryCount,
                        sleepDurationProvider =>
                            TimeSpan.FromSeconds(clientConfiguration.DelayBetweenRetriesInSeconds)));
        }

        private static void SetHttpClientProperties(
            HttpClient httpClient,
            HttpClientConfiguration clientConfiguration)
        {
            httpClient.BaseAddress = clientConfiguration.BaseAddress;
            if (!clientConfiguration.Timeout.HasValue)
                return;
            httpClient.Timeout = clientConfiguration.Timeout.Value;
        }

        private static HttpClientHandler CreateDefaultHttpClientHandler(
            X509Certificate x509Certificate)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                SslProtocols = SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback =
                    (Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>) (
                        (msg, cert, chain, err) =>
                            err == SslPolicyErrors.None || AseEndpointRegex.IsMatch(msg.RequestUri?.Host))
            };
            if (x509Certificate != null)
                httpClientHandler.ClientCertificates.Add(x509Certificate);
            return httpClientHandler;
        }

        private static readonly Regex AseEndpointRegex = new Regex("^[^.]+\\.azure\\.[^.]+\\.local$");
    }
}