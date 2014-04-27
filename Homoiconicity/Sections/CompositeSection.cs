using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class CompositeSection : IResumeSection
    {
        public List<IResumeSection> Sections { get; set; }

        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            var result = new List<IResumeElement>();

            foreach (var section in Sections)
            {
                var sectionElements = section.ProduceElements(resumeData);
                result.AddRange(sectionElements);
            }

            return result;
        }
    }
}