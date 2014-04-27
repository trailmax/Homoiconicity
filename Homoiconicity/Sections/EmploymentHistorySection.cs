using System;
using System.Collections.Generic;
using System.Linq;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class EmploymentHistorySection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData data)
        {
            if (!data.EmploymentHistories.Any())
            {
                yield break;
            }

            yield return new ResumeParagraph("Employment History").SetSectionHeaderStyle();


            var table = new ResumeTable(new float[] { 15, 15, 15, 60 })
                        {
                            new ResumeTableRow()
                            {
                                new ResumeTableCell("Period"),
                                new ResumeTableCell("Company"),
                                new ResumeTableCell("Position"),
                                new ResumeTableCell("Description"),
                            }
                        };

            foreach (var record in data.EmploymentHistories)
            {
                var row = new ResumeTableRow()
                          {
                              new ResumeTableCell(String.Format("{0:y} - {1:y}", record.StartDate, record.EndDate)),
                              new ResumeTableCell(record.EmployerName),
                              new ResumeTableCell(record.Position),
                              new ResumeTableCell(record.Description),
                          };
                table.Add(row);
            }

            yield return table;
        }
    }
}
