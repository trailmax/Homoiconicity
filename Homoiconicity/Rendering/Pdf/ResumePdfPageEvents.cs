using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Homoiconicity.Rendering.Pdf
{
    public class ResumePdfPageEvents : PdfPageEventHelper
    {
        private Font fontBlack;


        public override void OnEndPage(PdfWriter writer, Document document)
        {
            fontBlack = FontFactory.GetFont(PdfConverter.Verdana, 10f, new BaseColor(35, 31, 32));

            WriteFirstHeader(writer, document);

            WriteDefaultHeader(writer, document);

            WriteDefaultFooter(writer, document);
        }



        private void WriteFirstHeader(PdfWriter writer, Document document)
        {
            if (document.PageNumber != 1)
            {
                return;
            }

            var headerTable = new PdfPTable(2)
            {
                TotalWidth = document.PageSize.Width - document.LeftMargin,
                HorizontalAlignment = Element.ALIGN_CENTER,
            };

            headerTable.AddCell(new PdfPCell(new Phrase("First Header")));


            var verticalPosition = document.PageSize.Height - document.TopMargin + headerTable.TotalHeight - 10;
            headerTable.WriteSelectedRows(0, -1, 10, verticalPosition, writer.DirectContent);
        }


        private void WriteDefaultHeader(PdfWriter writer, Document document)
        {
            if (document.PageNumber <= 1)
            {
                return;
            }


            var headerTable = new PdfPTable(new float[] { 100})
                            {
                                TotalWidth = document.PageSize.Width - document.LeftMargin,
                                HorizontalAlignment = Element.ALIGN_RIGHT
                            };

            headerTable.AddCell(new PdfPCell { Border = Rectangle.NO_BORDER });

            headerTable.AddCell(new PdfPCell(new Phrase("Default Header")));


            var verticalPosition = document.PageSize.Height - document.TopMargin + headerTable.TotalHeight - 10;
            headerTable.WriteSelectedRows(0, -1, 10, verticalPosition, writer.DirectContent);
        }


        private void WriteDefaultFooter(PdfWriter writer, Document document)
        {
            var footer = new PdfPTable(new float[] { 96, 2, 2 })
            {
                TotalWidth = document.PageSize.Width - document.LeftMargin,
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            footer.AddCell(new PdfPCell(new Phrase("Default footer")));

            var pageNumber = document.PageNumber.ToString(CultureInfo.InvariantCulture);

            footer.AddCell(
                new PdfPCell(new Phrase(pageNumber, fontBlack))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.NO_BORDER,
                    });

            var verticalPosition = document.BottomMargin - 20;
            footer.WriteSelectedRows(0, -1, 10, verticalPosition, writer.DirectContent);
        }
    }
}