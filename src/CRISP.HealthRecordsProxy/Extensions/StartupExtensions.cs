using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using CRISP.HealthRecordProxy.Services;
using CRISP.HealthRecordsProxy.Common.Configurations;
using CRISP.HealthRecordsProxy.Common.Extensions;
using CRISP.HealthRecordsProxy.Repository.ImagingStudy;
using CRISP.HealthRecordsProxy.Repository.Observations;
using CRISP.HealthRecordsProxy.Repository.Specimen;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;
using CRISP.Storage.Object;
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
            serviceCollection.AddTransient<IResourceService, ResourceService>();
            serviceCollection.AddObservationClient(configuration);
            serviceCollection.AddSpecimenClient(configuration);
            serviceCollection.AddImagingStudyClient(configuration);


            serviceCollection.AddTransient<IObservationRepository, ObservationRepository>();
            serviceCollection.AddAzureBlobStorage<Guid, ObservationReportFhirModel>(configuration, "FHIRJSONStorage",
                "FHIRJSONStorage");

            serviceCollection.AddTransient<ISpecimenRepository, SpecimenRepository>();
            serviceCollection.AddAzureBlobStorage<Guid, SpecimenFhirModel>(configuration, "FHIRJSONStorage",
                "FHIRJSONStorage");

            serviceCollection.AddTransient<IImagingStudyRepository, ImagingStudyRepository>();
            serviceCollection.AddAzureBlobStorage<Guid, ImagingStudyFHIRModel>(configuration, "FHIRJSONStorage",
                "FHIRJSONStorage");
        }

        private static IServiceCollection AddImagingStudyClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            const string configurationName = "ImagingStudyClientConfiguration";
            const string clientName = "ImagingStudy";

            // add IHttpClientFactory for the web client
            var config = configuration
                .GetSection(configurationName)
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

        private static IServiceCollection AddSpecimenClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            const string observationClientConfiguration = "SpecimenClientConfiguration";
            const string clientName = "Specimen";

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

        private static IServiceCollection AddObservationClient(this IServiceCollection services,
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

        private static IHttpClientBuilder AddNamedHttpClient(
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

        public static IServiceCollection AddAzureBlobStorage<TKey, TValue>(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string configSection,
            string connectionStringName)
            where TValue : class
        {
            string connectionString = configuration.GetConnectionString(connectionStringName);
            return connectionString == "local"
                ? serviceCollection.AddSingleton<IStoreObjects<TKey, TValue>, MemoryObjectStorage<TKey, TValue>>()
                : serviceCollection
                    .AddSingleton<IProvideAzureBlobs<TKey>>(
                        (IProvideAzureBlobs<TKey>) new AzureBlobProvider<TKey>(configuration, connectionString,
                            configSection)).AddScoped<IStoreObjects<TKey, TValue>, AzureObjectStorage<TKey, TValue>>();
        }
    }
}