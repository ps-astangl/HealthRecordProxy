using CRISP.HealthRecordsProxy.Common.DomainModels;
using CRISP.Providers.Models.ImagingStudy;
using CRISP.Providers.Models.Observation;
using CRISP.Providers.Models.Specimen;

namespace CRISP.HealthRecordsProxy.Common.Mapping
{
    public static class ViewMapper
    {
        public static ObservationOverviewModel ToView(this ObservationReportFhirModel observation)
        {
            ObservationOverviewModel observationOverviewModel = new ObservationOverviewModel(observation);
            return observationOverviewModel;
        }
        public static SpecimenOverviewModel ToView(this SpecimenFhirModel specimen)
        {
            SpecimenOverviewModel specimenOverviewModel = new SpecimenOverviewModel(specimen);
            return specimenOverviewModel;
        }
        public static ImagingStudyOverviewModel ToView(this ImagingStudyFHIRModel imagingStudy)
        {
            ImagingStudyOverviewModel specimenOverviewModel = new ImagingStudyOverviewModel(imagingStudy);
            return specimenOverviewModel;
        }
    }
}