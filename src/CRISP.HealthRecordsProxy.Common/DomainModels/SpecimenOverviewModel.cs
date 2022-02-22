using CRISP.HealthRecordsProxy.Common.DomainModels.Abstraction;
using CRISP.HealthRecordsProxy.Common.Extensions;

namespace CRISP.HealthRecordsProxy.Common.DomainModels
{


    public class SpecimenOverviewModel : OverviewModel
    {
        public bool Flag { get; }
        public string Result { get; }
        public string Name { get; }

        /// <summary>
        /// Constructs overview model from CRISP FHIR resources
        /// </summary>
        public SpecimenOverviewModel(CRISP.Providers.Models.Specimen.SpecimenFhirModel specimen)
        {
            Flag = false;
            Result = specimen.Collection?.Quantity?.ToUiString() ?? string.Empty;
            Name = specimen.Type?.ToUiString() ?? string.Empty;
        }
    }
}