using System;
using System.Collections.Generic;
using System.IO;
using Homoiconicity.Data;
using Homoiconicity.Elements;
using Homoiconicity.Sections;

namespace Homoiconicity
{
    public abstract class RendererBase
    {
        public abstract MemoryStream CreateDocument(IEnumerable<IResumeSection> resumeSections, ResumeData data);
        protected abstract void RenderParagraph(ResumeParagraph resumeParagraph);
        protected abstract void RenderTable(ResumeTable resumeTable);
        protected abstract void RenderBulletedList(ResumeBulletedList bulletedList);


        protected void RenderElements(IEnumerable<IResumeSection> resumeSections, ResumeData data)
        {
            var elementRenderers = GetElementRenderers();

            foreach (var section in resumeSections)
            {
                var elements = section.ProduceElements(data);

                // do the rendering
                foreach (var element in elements)
                {
                    var elementRenderer = elementRenderers[element.GetType()];
                    elementRenderer.Invoke(element);
                }
            }
        }


        private Dictionary<Type, Action<IResumeElement>> GetElementRenderers()
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
