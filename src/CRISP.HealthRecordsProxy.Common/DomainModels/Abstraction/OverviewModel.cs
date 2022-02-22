using System;

namespace CRISP.HealthRecordsProxy.Common.DomainModels.Abstraction
{

    /// <summary>
    /// Abstract class representative of an overview of a FHIR model
    /// </summary>
    public abstract class OverviewModel
    {
    }

    public class OverviewModelKey : IEquatable<OverviewModelKey>
    {
        public const string Observation = "Observation";
        public const string Specimen = "Specimen";
        public const string ImagingStudy = "ImagingStudy";

        public string ResourceType { get; }
        public string ResourceId { get; }

        public OverviewModelKey(string resourceType, string resourceId)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        public bool Equals(OverviewModelKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ResourceType, other.ResourceType) && string.Equals(ResourceId, other.ResourceId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OverviewModelKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ResourceType != null ? ResourceType.GetHashCode() : 0) * 397) ^
                       (ResourceId != null ? ResourceId.GetHashCode() : 0);
            }
        }
    }
}