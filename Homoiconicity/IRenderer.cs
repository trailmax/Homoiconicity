using System.Collections.Generic;
using System.IO;
using Homoiconicity.Data;
using Homoiconicity.Elements;
using Homoiconicity.Sections;

namespace Homoiconicity
{
    /// <summary>
    /// Represent an object that actually creates an object out of a list of ResumeParts,
    /// and applies branding accordingly.
    /// </summary>
    public interface IRenderer
    {
        MemoryStream CreateDocument(IEnumerable<IResumeSection> resumeSections, ResumeData data);
        void RenderParagraph(ResumeParagraph resumeParagraph);
        void RenderTable(ResumeTable resumeTable);
        void RenderBulletedList(ResumeBulletedList bulletedList);
    }
}
