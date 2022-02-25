﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRISP.HealthRecordsProxy.Common.APIModels;
using CRISP.HealthRecordsProxy.Common.DomainModels;
using CRISP.HealthRecordsProxy.Common.Mapping;
using CRISP.HealthRecordsProxy.Repository.ImagingStudy;
using CRISP.HealthRecordsProxy.Repository.Observations;
using CRISP.HealthRecordsProxy.Repository.Specimen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRISP.HealthRecordProxy.Controllers
{
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly IObservationRepository _observationRepository;
        private readonly ISpecimenRepository _specimenRepository;
        private readonly IImagingStudyRepository _imagingStudyRepository;

        public DatabaseController(
            ILogger<DatabaseController> logger,
            IObservationRepository observationRepository,
            ISpecimenRepository specimenRepository,
            IImagingStudyRepository imagingStudyRepository)
        {
            _logger = logger;
            _observationRepository = observationRepository;
            _specimenRepository = specimenRepository;
            _imagingStudyRepository = imagingStudyRepository;
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetAllExample()
        {
            List<HealthRecordsRequest> healthRecordsRequests = new List<HealthRecordsRequest>();
            HealthRecordsRequest observationRequest = ExampleRequests.ObservationHealthRecordsRequest();
            HealthRecordsRequest specimenRequest = ExampleRequests.SpecimenHealthRecordsRequest();
            HealthRecordsRequest imagingStudyRequest = ExampleRequests.ImagingStudyHealthRecordsRequest();
            healthRecordsRequests.Add(observationRequest);
            healthRecordsRequests.Add(specimenRequest);
            healthRecordsRequests.Add(imagingStudyRequest);

            // Extract the observation ids
            var observationIds = healthRecordsRequests
                .Where(x => x.ResourceType.Equals("Observation", StringComparison.InvariantCultureIgnoreCase))
                .SelectMany(x => x.LogicalIdentifier);

            var resourceDict =
                healthRecordsRequests.ToDictionary(x => x.ResourceType, request => request.LogicalIdentifier);

            var observations = await _observationRepository.QueryByIds(resourceDict.GetValueOrDefault("Observation"));

            var specimens = await _specimenRepository.QueryByIds(resourceDict.GetValueOrDefault("Specimen"));
            var imagingStudies = await _imagingStudyRepository.QueryByIds(resourceDict.GetValueOrDefault("ImagingStudy"));

            HealthRecordsResponse healthRecordsResponse = new HealthRecordsResponse
            {
                Specimens = specimens?.Select(x => x.ToView()),
                Observations = observations?.Select(x => x.ToView()),
                ImagingStudy = imagingStudies.Select(x => x.ToView())
            };
            var stringResult = JsonConvert.SerializeObject(healthRecordsResponse);
            return new ContentResult
            {
                ContentType = "Application/json",
                Content = stringResult,
                StatusCode = 200
            };
        }


        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetObservationExample()
        {
            var request = ExampleRequests.ObservationHealthRecordsRequest();
            var ids = request.LogicalIdentifier.Select(x => x).ToList();

            var observations = await _observationRepository.QueryByIds(ids);
            var obs = observations
                ?.Where(x => x != null)
                ?.Select(x => x.ToView()) ?? new List<ObservationOverviewModel>();

            return CreateResponse(obs, null, null);
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetSpecimenExample()
        {
            var request = ExampleRequests.SpecimenHealthRecordsRequest();
            var ids = request.LogicalIdentifier.Select(x => x).ToList();
            var specimens = await _specimenRepository.QueryByIds(ids);

            var sps =specimens
                ?.Where(x => x != null)
                ?.Select(x => x.ToView()) ?? new List<SpecimenOverviewModel>();

            return CreateResponse(null, sps, null);
        }

        [HttpGet, Route("[action]")]
        public async Task<dynamic> GetImagingStudyExample()
        {
            var request = ExampleRequests.ImagingStudyHealthRecordsRequest();
            var ids = request.LogicalIdentifier.Select(x => x).ToList();

            var studies = await _imagingStudyRepository.QueryByIds(ids);
            var img =
                studies
                    ?.Where(x => x != null)
                    ?.Select(x => x.ToView()) ?? new List<ImagingStudyOverviewModel>();
            return CreateResponse(null, null, img);
        }

        private static dynamic CreateResponse(IEnumerable<ObservationOverviewModel> observations, IEnumerable<SpecimenOverviewModel> specimens, IEnumerable<ImagingStudyOverviewModel> imagingStudy)
        {
            HealthRecordsResponse healthRecordsResponse = new HealthRecordsResponse
            {
                Observations = observations,
                Specimens = specimens,
                ImagingStudy = imagingStudy
            };
            var stringResult = JsonConvert.SerializeObject(healthRecordsResponse);
            return new ContentResult
            {
                ContentType = "Application/json",
                Content = stringResult,
                StatusCode = 200
            };
        }
    }
}