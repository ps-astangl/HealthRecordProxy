using System;
using System.Security.Cryptography.X509Certificates;

namespace CRISP.HealthRecordsProxy.Common.Extensions
{

    public class HttpClientConfiguration
    {
        /// <summary>(Required) Name of HttpClient</summary>
        public string ClientName { get; }

        /// <summary>(Optional) Client certificate</summary>
        public X509Certificate2 X509Certificate2 { get; set; }

        /// <summary>(Optional) Base address for client.</summary>
        public Uri BaseAddress { get; set; }

        /// <summary>(Optional) HttpClient timeout</summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Constructor requiring only <see cref="P:CRISP.Extensions.DependencyInjection.HttpClientConfiguration.ClientName" />
        /// </summary>
        /// <param name="clientName">Name of the HttpClient to create</param>
        public HttpClientConfiguration(string clientName) => this.ClientName = clientName;

        /// <summary>Class constructor - requires ClientName.</summary>
        /// <param name="clientName"></param>
        /// <param name="x509Certificate2"></param>
        /// <param name="baseAddress"></param>
        /// <param name="timeout"></param>
        public HttpClientConfiguration(
            string clientName,
            X509Certificate2 x509Certificate2 = null,
            Uri baseAddress = null,
            TimeSpan? timeout = null)
        {
            this.ClientName = clientName;
            this.X509Certificate2 = x509Certificate2;
            this.BaseAddress = baseAddress;
            this.Timeout = timeout;
        }
    }
}