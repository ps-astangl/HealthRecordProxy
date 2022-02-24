using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.Fhir.Database;
using CRISP.HealthRecordsProxy.Repository.Context.ObservationContext;
using CRISP.HealthRecordsProxy.Repository.Context.ObservationContext.Models;
using CRISP.Providers.Models.Observation;
using CRISP.Storage.Object;
using Hl7.Fhir.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resource = CRISP.Fhir.Models.Resource;
using Task = System.Threading.Tasks.Task;

namespace CRISP.HealthRecordsProxy.Repository
{
    public interface IObservationRepository
    {
        public Task<IList<ObservationReportFhirModel>> QueryByIds(IEnumerable<Guid> ids);
    }
    public class ObservationRepository : IObservationRepository
    {
        private readonly ILogger<ObservationRepository> _logger;
        private readonly IStoreObjects<Guid, ObservationReportFhirModel> _store;
        private readonly ObservationContext _observationContext;

        public ObservationRepository(ILogger<ObservationRepository> logger, ObservationContext observationContext, IStoreObjects<Guid, ObservationReportFhirModel> store)
        {
            _logger = logger;
            _observationContext = observationContext;
            _store = store;
        }

        /// <inheritdoc />
        public async Task<IList<ObservationReportFhirModel>> QueryByIds(IEnumerable<Guid> ids)
        {
            return (await _observationContext.Observations.Where(s => ids.Contains(s.Id))
                    .Include(s => s.ObservationsJson)
                    .ToListAsync())
                .Select(obs => CreateFhirModel(obs).GetAwaiter().GetResult())
                .ToList();
        }

        private async Task<ObservationReportFhirModel> CreateFhirModel(Observations observations)
        {
            if (observations == null)
                throw new ArgumentNullException("Observations", "Cant be null");

            var observationsJson = observations?.ObservationsJson?.FirstOrDefault()?.Response;

            if (!string.IsNullOrWhiteSpace(observationsJson))
            {
                return JsonConvert.DeserializeObject<ObservationReportFhirModel>(observationsJson);
            }

            try
            {
                var result = await _store.GetValue(observations.Id);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to get from storage");
                throw;
            }
        }
    }
}