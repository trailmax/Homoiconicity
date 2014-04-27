using System;
using System.Collections.Generic;
using System.Linq;
using Homoiconicity.Data;
using Homoiconicity.Elements;
using Homoiconicity.Specifications;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class ConditionalSection : IResumeSection
    {
        public IResumeSection TruthSection { get; private set; }
        public IResumeSectionSpecification SectionSpecification { get; private set; }


        public ConditionalSection(IResumeSectionSpecification sectionSpecification, IResumeSection truthSection)
        {
            this.TruthSection = truthSection;
            this.SectionSpecification = sectionSpecification;
        }


        public IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData)
        {
            if (SectionSpecification.IsSatisfiedBy(resumeData))
            {
                return TruthSection.ProduceElements(resumeData);
            }
            return Enumerable.Empty<IResumeElement>();
        }
    }
}
