using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class PersonalDetailsSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData data)
        {
            yield return new ResumeParagraph(data.DocumentTitle)
                    .SetSectionHeaderStyle()
                    .SetHorisontalAlignment(ElementAlignmnet.Center);

            var relativeColumnWidths = new float[] { 25, 25, 25, 25 };
            yield return new ResumeTable(relativeColumnWidths)
                         {
                             new ResumeTableRow()
                             {
                                 new ResumeTableCell("Telephone"),
                                 new ResumeTableCell(data.Telephone),
                                 new ResumeTableCell("Email"),
                                 new ResumeTableCell(data.Email),
                             },
                             new ResumeTableRow()
                             {
                                 new ResumeTableCell("Experience"),
                                 new ResumeTableCell(data.ExperienceYears + " year(s)"),
                                 new ResumeTableCell("Country of Residence"),
                                 new ResumeTableCell(data.ResidenceCountry),
                             }
                         };
        }
    }
}
