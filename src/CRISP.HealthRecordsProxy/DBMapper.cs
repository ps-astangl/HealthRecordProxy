using System.Linq;
using CRISP.HealthRecordsProxy.Common.DomainModels;
using Microsoft.AspNetCore.Builder;

namespace CRISP.HealthRecordProxy
{
    public static class DBMapper
    {
        public static ObservationOverviewModel Map(
            this CRISP.HealthRecordsProxy.Repository.Context.ObservationContext.Models.Observations observations)
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