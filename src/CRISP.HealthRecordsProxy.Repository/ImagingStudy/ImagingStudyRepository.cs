using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordsProxy.Repository.ImagingStudy.Context;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Storage.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordsProxy.Repository.ImagingStudy
{
    public interface IImagingStudyRepository
    {
        public Task<IList<ImagingStudyFHIRModel>> QueryByIds(IEnumerable<Guid> ids);
    }
    public class ImagingStudyRepository : IImagingStudyRepository
    {
        private readonly ILogger<ImagingStudyRepository> _logger;
        private readonly IStoreObjects<Guid, ImagingStudyFHIRModel> _store;
        private readonly ImagingStudyContext _context;

        public ImagingStudyRepository(ILogger<ImagingStudyRepository> logger, IStoreObjects<Guid, ImagingStudyFHIRModel> store, ImagingStudyContext context)
        {
            _logger = logger;
            _store = store;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IList<ImagingStudyFHIRModel>> QueryByIds(IEnumerable<Guid> ids)
        {
            if (ids == null) return new List<ImagingStudyFHIRModel>();

            return (await _context.ImagingStudy
                    .Where(study => ids.Contains(study.Id))
                    .Include(study => study.ImagingStudyJson)
                    .Include(study => study.ImagingStudyEndpoint)
                    .ToListAsync())
                .Select(study => CreateFhirModel(study).GetAwaiter().GetResult())
                .ToList();
        }
        private async Task<ImagingStudyFHIRModel> CreateFhirModel(Repository.Context.Models.ImagingStudy imagingStudy)
        {
            if (imagingStudy == null)
               return new ImagingStudyFHIRModel();

            var imagingStudyJson = imagingStudy?.ImagingStudyJson?.FirstOrDefault()?.Response;

            if (!string.IsNullOrWhiteSpace(imagingStudyJson))
                return JsonConvert.DeserializeObject<ImagingStudyFHIRModel>(imagingStudyJson);

            try
            {
                var result = await _store.GetValue(imagingStudy.Id);
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