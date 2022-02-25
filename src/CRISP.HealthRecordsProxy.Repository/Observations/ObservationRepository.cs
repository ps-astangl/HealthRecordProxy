using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordsProxy.Repository.Observations.Context;
using CRISP.Providers.Models.Observation;
using CRISP.Storage.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordsProxy.Repository.Observations
{
    public interface IObservationRepository
    {
        public Task<IEnumerable<ObservationReportFhirModel>> QueryByIds(IEnumerable<Guid> ids);
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
        public async Task<IEnumerable<ObservationReportFhirModel>> QueryByIds(IEnumerable<Guid> ids)
        {
            return (await _observationContext.Observations.Where(s => ids.Contains(s.Id))
                    .Include(s => s.ObservationsJson)
                    .ToListAsync())
                .Select(obs => CreateFhirModel(obs).GetAwaiter().GetResult())
                .ToList();
        }

        private async Task<ObservationReportFhirModel> CreateFhirModel(Context.Models.Observations observations)
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