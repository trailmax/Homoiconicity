using System;

namespace Homoiconicity.Elements
{
    public class ResumeFont
    {
        public float Size { get; private set; }
        public ResumeFontWeight FontWeight { get; private set; }


        public ResumeFont()
        {
            Size = 10f;
            FontWeight = ResumeFontWeight.Normal;
        }

        public ResumeFont SetSize(Single fontSize)
        {
            Size = fontSize;
            return this;
        }

        public ResumeFont SetFontWeight(ResumeFontWeight fontWeight)
        {
            FontWeight = fontWeight;
            return this;
        }
    }


    public enum ResumeFontWeight
    {
        Bold,
        Normal,
        BoldItalic,
        Italic
    }
}
