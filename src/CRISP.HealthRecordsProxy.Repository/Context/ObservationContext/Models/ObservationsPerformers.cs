﻿using System;

namespace CRISP.HealthRecordsProxy.Repository.Context.ObservationContext.Models
{
    public partial class ObservationsPerformers
    {
        public string PerformerId { get; set; }
        public Guid ObservationId { get; set; }

        public Observations Observation { get; set; }
    }
}
