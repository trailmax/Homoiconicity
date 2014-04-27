using System;
using Homoiconicity.Data;

namespace Homoiconicity.Specifications
{
    [Serializable]
    public class CitizenSecretSpecification : IResumeSectionSpecification
    {
        public bool IsSatisfiedBy(ResumeData data)
        {
            return data.ReaderClearanceLevel == ClearanceLevel.LoyalCitizen || data.ReaderClearanceLevel == ClearanceLevel.TopSecret;
        }
    }
}