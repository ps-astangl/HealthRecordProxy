using CRISP.Fhir.Models;
using CRISP.HealthRecordProxy.Extensions;
using CRISP.HealthRecordsProxy.Repository.ImagingStudy.Context;
using CRISP.HealthRecordsProxy.Repository.Observations.Context;
using CRISP.HealthRecordsProxy.Repository.Specimen.Context;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddDbContextPool<ObservationContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:ObservationContext"]);
                options.EnableSensitiveDataLogging();
            });
            services.AddDbContextPool<SpecimenContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:SpecimenContext"]);
                options.EnableSensitiveDataLogging();
            });
            services.AddDbContextPool<ImagingStudyContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:ImagingStudyContext"]);
                options.EnableSensitiveDataLogging();
            });
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