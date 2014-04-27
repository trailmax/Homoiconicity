using System;

namespace Homoiconicity.Elements
{
    public class ResumeParagraph : IResumeElement
    {
        public ResumeFont ResumeFont { get; private set; }
        public readonly String Text;
        public ElementAlignmnet HorisontalAlignment { get; private set; }


        public ResumeParagraph(string text)
        {
            Text = text;
            ResumeFont = new ResumeFont();
            HorisontalAlignment = ElementAlignmnet.Left;
        }

        public ResumeParagraph SetFont(ResumeFont resumeFont)
        {
            this.ResumeFont = resumeFont;
            return this;
        }

        public ResumeParagraph SetHorisontalAlignment(ElementAlignmnet horisontalAlignment)
        {
            this.HorisontalAlignment = horisontalAlignment;
            return this;
        }

        public ResumeParagraph SetSectionHeaderStyle()
        {
            this.SetFont(new ResumeFont()
                .SetSize(18)
                .SetFontWeight(ResumeFontWeight.Bold));

            return this;
        }
    }
}
