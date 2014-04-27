using System;
using Homoiconicity.Data;

namespace Homoiconicity.Specifications
{
    [Serializable]
    public class TopSecretSpecification : IResumeSectionSpecification
    {
        public bool IsSatisfiedBy(ResumeData data)
        {
            return data.ReaderClearanceLevel == ClearanceLevel.TopSecret;
        }
    }
}