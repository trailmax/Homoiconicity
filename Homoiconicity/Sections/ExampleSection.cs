using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    public class ExampleSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            yield return new ResumeParagraph("Sample text for demonstration");
            yield return new ResumeBulletedList()
                         {
                             Paragraphs = new List<ResumeParagraph>()
                                          {
                                              new ResumeParagraph("Bullet Point One"),
                                              new ResumeParagraph("Bullet Point Two"),
                                          }
                         };
        }
    }
}
