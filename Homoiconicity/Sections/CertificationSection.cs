using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class CertificationSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            yield return new ResumeParagraph("Certifications").SetSectionHeaderStyle();

            var bulletedList = new ResumeBulletedList();

            foreach (var certification in resumeData.Certifications)
            {
                var text = String.Format("{0} {1:y} - {2:y}", certification.CertificationName, certification.StartDate, certification.ExpiryDate);
                bulletedList.AddParagraph(text);
            }

            yield return bulletedList;
        }
    }
}
