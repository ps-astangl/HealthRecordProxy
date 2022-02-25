using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.HealthRecordsProxy.Common.DomainModels;
using CRISP.HealthRecordsProxy.Common.Mapping;
using CRISP.HealthRecordsProxy.Repository.Observations;
using CRISP.HealthRecordsProxy.Repository.Observations.Context;
using CRISP.HealthRecordsProxy.Repository.Observations.Context.Models;
using CRISP.HealthRecordsProxy.Repository.Specimen;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordProxy.Controllers
{
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly IObservationRepository _observationRepository;
        private readonly ISpecimenRepository _specimenRepository;

        public DatabaseController(ILogger<DatabaseController> logger, IObservationRepository observationRepository,
            ISpecimenRepository specimenRepository)
        {
            _logger = logger;
            _observationRepository = observationRepository;
            _specimenRepository = specimenRepository;
        }

        #region Observation Examples

        [HttpGet, Route("[action]")]
        public async Task<dynamic> ObservationStoreTest([FromServices] IObservationRepository context)
        {
            var request = ObservationHealthRecordsRequest();
            var ids = request.LogicalIdentifier.Select(Guid.Parse).ToList();

            var observations = await context.QueryByIds(ids);
            return observations.Select(x => x.ToView());
        }

        [HttpPost, Route("[action]")]
        public async Task<dynamic> Search([FromBody] IEnumerable<HealthRecordsDbRequest> requestModel)
        {
            var ids = requestModel.SelectMany(x => x.LogicalIdentifier);
            var observations = await _observationRepository.QueryByIds(ids);
            return observations.Select(x => x.ToView());
        }

        #endregion

        #region Specimen Examples

        [HttpGet, Route("[action]")]
        public async Task<dynamic> ObservationDatabaseTest()
        {
            var request = ObservationHealthRecordsRequest();
            var ids = request.LogicalIdentifier.Select(Guid.Parse).ToList();
            var obs = await _observationRepository.QueryByIds(ids);

            var result = obs
                ?.Where(x => x != null)
                ?.Select(x => x.ToView()) ?? new List<ObservationOverviewModel>();
            return Ok(result);
        }


        [HttpGet, Route("[action]")]
        public async Task<dynamic> SpecimenDatabaseTest()
        {
            var request = SpecimenHealthRecordsRequest();
            var ids = request.LogicalIdentifier.Select(Guid.Parse).ToList();
            var spm = await _specimenRepository.QueryByIds(ids);

            var result = spm
                ?.Where(x => x != null)
                ?.Select(x => x.ToView()) ?? new List<SpecimenOverviewModel>();

            return Ok(result);
        }

        #endregion



        #region RequestExamples
        private static HealthRecordsRequest ObservationHealthRecordsRequest()
        {
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(Observation),
                LogicalIdentifier = new string[]
                {
                    "21748C07-AA58-A460-8518-1A93F48F424F",
                    "C1258861-497D-D89B-C833-17E5CBB246F5",
                    "73B70122-B0F9-5EF9-C55F-9855D0F0AB99",
                    "7F509AFB-6E6A-E11F-6250-4AAEAB51A1BF",
                    "5C0D29A3-7D67-7441-8901-CEC65AFAD2AE",
                    "D2112447-4D52-AD41-7D6A-A9AB63CE5EA9",
                    "54F4B253-C2E2-EF81-AFDA-2A9919411E59",
                    "429FFBD4-0059-3DAD-468A-ABB56C82FF85",
                    "692537FB-C2E8-1FE1-BCB5-BC81A80E18A6",
                    "F20005DB-3C5D-817A-6AFE-3DE73488EE9E"
                }
            };
            return healthRecordsRequest;
        }

        private static HealthRecordsRequest SpecimenHealthRecordsRequest()
        {
            HealthRecordsRequest healthRecordsRequest = new HealthRecordsRequest
            {
                ResourceType = nameof(Specimen),
                LogicalIdentifier = new string[]
                {
                    "FF50D2A0-E3EE-5CBE-916E-E0F5A535CDC1",
                    "B3D86F8B-8FDD-C1F6-F7C4-3AC21CD66418",
                    "44D79F55-F0DF-5059-D6BA-F677092C1EA0",
                    "0E08FEEA-2DB7-8F02-1471-351962F3974D",
                    "9B4841DE-1EF5-8B5C-9D23-E12048572186",
                    "AEAEAE5B-4DB2-3BF6-A7CF-A84810B85612",
                    "603D3038-4AA8-D5FD-93F3-A3A020862202",
                    "5439307F-9F67-A701-651F-639FE3CF4B57",
                    "CB7125AB-C516-BDF1-22B1-80141D1815A7",
                    "E4A6A8C4-E7C1-D5FC-B231-4507BA3A193F"
                }
            };
            return healthRecordsRequest;
        }

        #endregion

    }
}