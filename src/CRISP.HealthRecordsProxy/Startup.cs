using CRISP.Fhir.Models;
using CRISP.HealthRecordProxy.Extensions;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;
using CRISP.Telemetry.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CRISP.HealthRecordProxy
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.EnableEndpointRouting = false; });
            services.AddResourceService(_configuration);
            EntryJsonConverter.AddOrUpdateMapping<ObservationReportFhirModel>("Observation");
            EntryJsonConverter.AddOrUpdateMapping<SpecimenFhirModel>("Specimen");
            EntryJsonConverter.AddOrUpdateMapping<ImagingStudyFHIRModel>("ImagingStudy");
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}