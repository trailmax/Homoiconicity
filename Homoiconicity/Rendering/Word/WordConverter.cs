using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Homoiconicity.Elements;

namespace Homoiconicity.Rendering.Word
{
    public static class WordConverter
    {
        public static Paragraph GetWordParagraph(ResumeParagraph resumeParagraph)
        {
            var wordParagraph = new Paragraph();
            wordParagraph.AppendChild(GetParagraphProperties(resumeParagraph));

            var run = wordParagraph.AppendChild(new Run());
            var runProperties = GetRunProperties(resumeParagraph.ResumeFont);
            run.AppendChild(runProperties);

            run.Append(CreateLineBreaks(resumeParagraph.Text));

            return wordParagraph;
        }


        public static IEnumerable<OpenXmlLeafElement> CreateLineBreaks(string textualData)
        {
            var textArray = textualData.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);

            var first = true;

            foreach (var line in textArray)
            {
                if (!first)
                {
                    yield return new Break();
                }

                first = false;

                yield return new Text(line);
            }
        }



        public static ParagraphProperties GetParagraphProperties(ResumeParagraph resumeParagraph, IEnumerable<OpenXmlElement> insertBefore = null)
        {
            var pargraphProperties = new ParagraphProperties();
            if (insertBefore != null)
            {
                pargraphProperties.Append(insertBefore);
            }

            pargraphProperties.AppendChild(GetJustification(resumeParagraph.HorisontalAlignment));

            return pargraphProperties;
        }


        public static Justification GetJustification(ElementAlignmnet elementAlignment)
        {
            var justification = new Justification();
            switch (elementAlignment)
            {
                case ElementAlignmnet.Center:
                    justification.Val = JustificationValues.Center;
                    break;
                case ElementAlignmnet.Left:
                    justification.Val = JustificationValues.Left;
                    break;
                case ElementAlignmnet.Right:
                    justification.Val = JustificationValues.Right;
                    break;
                default:
                    justification.Val = JustificationValues.Left;
                    break;
            }

            return justification;
        }



        public static RunProperties GetRunProperties(ResumeFont resumeFont)
        {
            var runProperties = new RunProperties();

            if (resumeFont.FontWeight == ResumeFontWeight.Bold)
            {
                var bold = new Bold
                {
                    Val = OnOffValue.FromBoolean(true)
                };
                runProperties.AppendChild(bold);
            }

            runProperties.AppendChild(GetFontSize(resumeFont));


            return runProperties;
        }


        public static FontSize GetFontSize(ResumeFont resumeFont)
        {
            var fontSize = new FontSize();
            fontSize.Val = new StringValue((resumeFont.Size * 2f).ToString());
            return fontSize;
        }
    }
}