using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordsProxy.Repository.Specimen.Context;
using CRISP.HealthRecordsProxy.Repository.Specimen.Context.Models;
using CRISP.Providers.Models.Specimen;
using CRISP.Storage.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordsProxy.Repository.Specimen
{
    public interface ISpecimenRepository
    {
        public Task<IList<SpecimenFhirModel>> QueryByIds(IEnumerable<Guid> ids);
    }
    public class SpecimenRepository : ISpecimenRepository
    {
        private readonly ILogger<SpecimenRepository> _logger;
        private readonly IStoreObjects<Guid, SpecimenFhirModel> _store;
        private readonly SpecimenContext _specimenContext;

        public SpecimenRepository(ILogger<SpecimenRepository> logger, IStoreObjects<Guid, SpecimenFhirModel> store, SpecimenContext specimenContext)
        {
            _logger = logger;
            _store = store;
            _specimenContext = specimenContext;
        }

        /// <inheritdoc />
        public async Task<IList<SpecimenFhirModel>> QueryByIds(IEnumerable<Guid> ids)
        {
            return (await _specimenContext.Specimens
                    .Where(s => ids.Contains(s.Id))
                    .Include(s => s.SpecimensJson)
                    .Include(s => s.ParentSpecimensSpecimen)
                    .ToListAsync())
                .Select(s => CreateFhirModel(s).GetAwaiter().GetResult())
                .ToList();
        }
        private async Task<SpecimenFhirModel> CreateFhirModel(Specimens specimen)
        {
            if (specimen == null)
               return new SpecimenFhirModel();

            var specimenJson = specimen?.SpecimensJson?.FirstOrDefault()?.Response;

            if (!string.IsNullOrWhiteSpace(specimenJson))
            {
                return JsonConvert.DeserializeObject<SpecimenFhirModel>(specimenJson);
            }

            try
            {
                var result = await _store.GetValue(specimen.Id);
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