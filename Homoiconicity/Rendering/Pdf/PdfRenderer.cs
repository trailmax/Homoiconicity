using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Homoiconicity.Data;
using Homoiconicity.Elements;
using Homoiconicity.Sections;
using Homoiconicity.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Homoiconicity.Rendering.Pdf
{
    public class PdfRenderer : RendererBase
    {
        private const string InconsolataTtf = "Inconsolata.ttf";
        private readonly IServerPathMapper mapperPathMapper;
        private Document document;


        public PdfRenderer(IServerPathMapper mapperPathMapper)
        {
            this.mapperPathMapper = mapperPathMapper;
        }


        public override MemoryStream CreateDocument(IEnumerable<IResumeSection> resumeSections, ResumeData data)
        {
            byte[] resultBytes;

            FontFactory.RegisterDirectories();

            var fontPath = mapperPathMapper.MapPath(InconsolataTtf);
            FontFactory.Register(fontPath, PdfConverter.Verdana);

            var pdfOutputStream = new MemoryStream();
            document = new Document(PageSize.A4);
            document.SetMargins(20, 20, 20, 20);



            PdfWriter.GetInstance(document, pdfOutputStream);

            document.AddTitle(data.DocumentTitle);
            document.Open();

            base.RenderElements(resumeSections, data);

            document.Close();
            resultBytes = pdfOutputStream.ToArray();
            pdfOutputStream.Close();
            pdfOutputStream.Dispose();
            return new MemoryStream(resultBytes);
        }



        protected override void RenderParagraph(ResumeParagraph resumeParagraph)
        {
            var pdfFont = PdfConverter.GetPdfFont(resumeParagraph.ResumeFont);

            var paragraph = new Paragraph(resumeParagraph.Text, pdfFont)
            {
                Alignment = PdfConverter.TextAlignment(resumeParagraph.HorisontalAlignment),
            };

            this.document.Add(paragraph);
        }



        protected override void RenderTable(ResumeTable resumeTable)
        {
            var table = new PdfPTable(resumeTable.RelativeColumnWidths)
            {
                WidthPercentage = resumeTable.WidthPercentage,
                HeaderRows = 0,
                SpacingBefore = 10f,
                SpacingAfter = 10f,
                SplitLate = false,
                SplitRows = false,
                HorizontalAlignment = PdfConverter.TextAlignment(resumeTable.HorisontalAlignment),
            };

            var cells = resumeTable.SelectMany(cell => cell);
            foreach (var resumeTableCell in cells)
            {
                var cell = PdfConverter.CreateCell(resumeTableCell);
                table.AddCell(cell);
            }

            this.document.Add(table);
        }


        protected override void RenderBulletedList(ResumeBulletedList bulletedList)
        {
            var pdfList = new List(List.UNORDERED, 10f);

            var bulletPointFont = FontFactory.GetFont(PdfConverter.Verdana, 10f, Font.BOLD);

            foreach (var resumeParagraph in bulletedList.Paragraphs)
            {
                var li = new ListItem(PdfConverter.GetPhrase(resumeParagraph))
                {
                    ListSymbol = new Chunk("\u2022", bulletPointFont),
                    IndentationLeft = 10f
                };

                pdfList.Add(li);
            }

            this.document.Add(pdfList);
        }
    }
}