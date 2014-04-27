using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class EducationSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            yield return new ResumeParagraph("Education").SetSectionHeaderStyle();

            foreach (var education in resumeData.Educations)
            {
                var header = String.Format("{0:y} - {1:y} {2}", education.StartDate, education.EndDate, education.Establishment);
                yield return new ResumeParagraph(header).SetFont(new ResumeFont()
                                                                        .SetSize(12)
                                                                        .SetFontWeight(ResumeFontWeight.Bold));

                var details = String.Format("{0} {1} {2}", education.DegreeLevel, education.CourseName, education.AcheivementLevel);
                yield return new ResumeParagraph(details);
            }
        }
    }
}
