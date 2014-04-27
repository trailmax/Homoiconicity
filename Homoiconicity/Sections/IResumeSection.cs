using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    /// <summary>
    /// Larger part of a resume, consisting of a small chunks of text or other elements.
    /// </summary>
    public interface IResumeSection
    {
        IEnumerable<IResumeElement> ProduceElements(ResumeData resumeData);
    }
}