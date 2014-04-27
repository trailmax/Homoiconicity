using Homoiconicity.Elements;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Homoiconicity.Rendering.Pdf
{
    public static class PdfConverter
    {
        public const string Verdana = "Verdana";

        public static Font GetPdfFont(ResumeFont resumeFont)
        {
            var pdfFont = FontFactory.GetFont(
                Verdana,
                resumeFont.Size,
                FontWeight(resumeFont.FontWeight));

            return pdfFont;
        }


        public static int FontWeight(ResumeFontWeight fontWeight)
        {
            switch (fontWeight)
            {
                case ResumeFontWeight.Bold:
                    return Font.BOLD;
                case ResumeFontWeight.BoldItalic:
                    return Font.BOLDITALIC;
                case ResumeFontWeight.Italic:
                    return Font.ITALIC;
                default:
                    return Font.NORMAL;
            }
        }


        public static int TextAlignment(ElementAlignmnet alignment)
        {
            switch (alignment)
            {
                case ElementAlignmnet.Center:
                    return Element.ALIGN_CENTER;
                case ElementAlignmnet.Right:
                    return Element.ALIGN_RIGHT;
                case ElementAlignmnet.Left:
                    return Element.ALIGN_LEFT;
                default:
                    return Element.ALIGN_LEFT;
            }
        }


        public static Phrase GetPhrase(ResumeParagraph resumeParagraph)
        {
            var font = GetPdfFont(resumeParagraph.ResumeFont);
            var result = new Phrase(resumeParagraph.Text, font);
            return result;
        }


        public static PdfPCell CreateCell(ResumeTableCell resumeCell)
        {
            var cell = new PdfPCell(GetPhrase(resumeCell.Paragraph))
                           {
                               HorizontalAlignment = TextAlignment(resumeCell.HorisontalAlignment),
                               Rowspan = 1,
                               Colspan = 1,
                               Border = 0,
                               PaddingTop = 4f,
                               PaddingBottom = 4f,
                               PaddingLeft = 4f,
                               PaddingRight = 4f,
                           };

            return cell;
        }
    }
}