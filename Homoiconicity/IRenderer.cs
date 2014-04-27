using System;
using System.Collections.Generic;
using System.IO;
using Homoiconicity.Data;
using Homoiconicity.Elements;
using Homoiconicity.Sections;

namespace Homoiconicity
{
    public abstract class IRenderer
    {
        public abstract MemoryStream CreateDocument(IEnumerable<IResumeSection> resumeSections, ResumeData data);
        protected abstract void RenderParagraph(ResumeParagraph resumeParagraph);
        protected abstract void RenderTable(ResumeTable resumeTable);
        protected abstract void RenderBulletedList(ResumeBulletedList bulletedList);


        protected Dictionary<Type, Action<IResumeElement>> GetElementRenderers()
        {
            var result = new Dictionary<Type, Action<IResumeElement>>()
                             {
                                 { typeof(ResumeParagraph), (element) => RenderParagraph((ResumeParagraph)element) },
                                 { typeof(ResumeTable), (element) => RenderTable((ResumeTable)element) },
                                 { typeof(ResumeBulletedList), (element) => RenderBulletedList((ResumeBulletedList)element) }
                             };
            return result;
        }
    }
}
