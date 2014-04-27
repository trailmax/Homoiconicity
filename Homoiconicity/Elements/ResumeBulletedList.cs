using System.Collections.Generic;

namespace Homoiconicity.Elements
{
    public class ResumeBulletedList : IResumeElement
    {
        public List<ResumeParagraph> Paragraphs { get; set; }


        public ResumeBulletedList()
        {
            Paragraphs = new List<ResumeParagraph>();
        }

        public ResumeBulletedList AddParagraphs(IEnumerable<ResumeParagraph> resumeParagraphs)
        {
            Paragraphs.AddRange(resumeParagraphs);

            return this;
        }


        public ResumeBulletedList AddParagraph(ResumeParagraph resumeParagraph)
        {
            Paragraphs.Add(resumeParagraph);

            return this;
        }


        public ResumeBulletedList AddParagraph(string text)
        {
            Paragraphs.Add(new ResumeParagraph(text));

            return this;
        }
    }
}
