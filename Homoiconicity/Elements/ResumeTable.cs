using System.Collections.Generic;
using System.Linq;

namespace Homoiconicity.Elements
{
    public class ResumeTable : List<ResumeTableRow>, IResumeElement
    {
        private readonly float[] relativeColumnWidths;
        public float[] RelativeColumnWidths
        {
            get
            {
                var total = relativeColumnWidths.Sum();
                var result = new List<float>();

                for (var i = 0; i < relativeColumnWidths.Length; i++)
                {
                    var origValue = relativeColumnWidths[i];
                    result.Add((origValue / total) * 100f);
                }

                return result.ToArray();
            } 
        }


        public float WidthPercentage { get; private set; }
        public ElementAlignmnet HorisontalAlignment { get; private set; }

        public ResumeTable(float[] relativeColumnWidths)
        {
            this.relativeColumnWidths = relativeColumnWidths;
            WidthPercentage = 100f;
            HorisontalAlignment = ElementAlignmnet.Left;
        }

        public ResumeTable SetWidthPercentage(float widthPercentage)
        {
            WidthPercentage = widthPercentage;
            return this;
        }

        public ResumeTable SetHorisontalAlignment(ElementAlignmnet horisontalAlignment)
        {
            HorisontalAlignment = horisontalAlignment;
            return this;
        }
    }


    public class ResumeTableRow : List<ResumeTableCell>
    {
        // nothing here
    }


    public class ResumeTableCell
    {
        public readonly ResumeParagraph Paragraph;
        public ElementAlignmnet HorisontalAlignment { get; private set; }

        public ResumeTableCell(string text)
        {
            Paragraph = new ResumeParagraph(text);
            SetDefaults();
        }


        public ResumeTableCell(ResumeParagraph paragraph)
        {
            Paragraph = paragraph;
            SetDefaults();
        }

        private void SetDefaults()
        {
            HorisontalAlignment = ElementAlignmnet.Left;
        }

        public ResumeTableCell SetHorisontalAlignment(ElementAlignmnet horisontalAlignment)
        {
            HorisontalAlignment = horisontalAlignment;
            return this;
        }
    }
}
