namespace CRISP.HealthRecordsProxy.Common.Configurations
{

    public class ClientConfiguration
    {
        /// <summary>
        /// Base URL of the endpoint.
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Thumbprint of the certificate to use.
        /// It's preferable to use the certificate's
        /// common name if possible.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Common Name of the certificate to use.
        /// It's preferable to use this over the
        ///  certificate's thumbprint if possible.
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Timeout for request calls to the Care
        /// Encounter endpoint.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 59;

        /// <summary>
        /// Number of attempts to re-try a failed HttpRequest:
        /// <list type="bullet">
        /// <item><description>HTTP 5XX status codes (server errors)</description></item>
        /// <item><description>HTTP 408 status code (request timeout)</description></item>
        /// </list>
        /// <remarks>
        /// Default value is set to 5 tries
        /// </remarks>
        /// </summary>
        public int RetryCount { get; set; } = 5;

        /// <summary>
        /// The number of second between a re-try for transient network failures:
        /// <list type="bullet">
        /// <item><description>HTTP 5XX status codes (server errors)</description></item>
        /// <item><description>HTTP 408 status code (request timeout)</description></item>
        /// </list>
        /// <remarks>
        /// Default value is 5 seconds
        /// </remarks>
        /// </summary>
        public int DelayBetweenRetriesInSeconds { get; set; } = 5;

        /// <summary>
        /// Sets the length of time that a HttpMessageHandler instance can be reused.
        /// Each named client can have its own configured handler lifetime value expressed in minutes
        /// <remarks>
        /// Default is 2 minutes.
        /// </remarks>
        /// </summary>
        public int RetryHandlerLifeTimeInMinutes { get; set; } = 2;
    }
}