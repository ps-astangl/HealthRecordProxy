using System.Linq;
using CRISP.HealthRecordsProxy.Common.DomainModels;
using CRISP.HealthRecordsProxy.Repository.Observations.Context.Models;
using Microsoft.AspNetCore.Builder;

namespace CRISP.HealthRecordProxy
{
    public static class DBMapper
    {
        public static ObservationOverviewModel Map(
            this Observations observations)
        {
            var overviewModel = new ObservationOverviewModel
            {
                Name = "Not Present",
                Result = $"{observations.ValueQuantity} {observations.ValueQuantityUnits}",
                Range = "NOT PRESENT",
                Comment = "NOT PRESENT",
                Status = observations.Status,
                Interpretation = observations.AbnormalFlag,
                Issued = observations.DateIssued.ToString()
            };
            return overviewModel;
        }
    }
}