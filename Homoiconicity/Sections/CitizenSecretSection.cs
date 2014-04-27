using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class CitizenSecretSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            yield return new ResumeParagraph("Citizen Secrets").SetSectionHeaderStyle();

            foreach (var secret in resumeData.CitizenSecrets)
            {
                yield return new ResumeParagraph(secret.SecretInformation);
            }
        }
    }
}