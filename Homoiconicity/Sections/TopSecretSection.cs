using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class TopSecretSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            yield return new ResumeParagraph("Top Secrets").SetSectionHeaderStyle();

            foreach (var secret in resumeData.TopSecrets)
            {
                yield return new ResumeParagraph(secret.SecretInformation);
            }
        }
    }
}
