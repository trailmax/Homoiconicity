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
    public class PdfRenderer : IRenderer
    {
        private const string InconsolataTtf = "Inconsolata.ttf";
        private readonly ILoggingService logger;
        private readonly IServerPathService servicePathService;
        private Document document;


        public PdfRenderer(ILoggingService logger, IServerPathService servicePathService)
        {
            this.logger = logger;
            this.servicePathService = servicePathService;
        }


        public MemoryStream CreateDocument(IEnumerable<IResumeSection> resumeSections, ResumeData data)
        {
            byte[] resultBytes;

            FontFactory.RegisterDirectories();

            var fontPath = servicePathService.MapPath(InconsolataTtf);
            FontFactory.Register(fontPath, PdfConverter.Verdana);

            var pdfOutputStream = new MemoryStream();
            document = new Document(PageSize.A4);
            document.SetMargins(20, 20, 20, 20);



            PdfWriter.GetInstance(document, pdfOutputStream);

            document.AddTitle(data.DocumentTitle);
            document.Open();

            var elementRenderers = GetElementRenderers();

            foreach (var section in resumeSections)
            {
                var elements = section.ProduceElements(data);

                // do the rendering
                foreach (var element in elements)
                {
                    if (!elementRenderers.ContainsKey(element.GetType()))
                    {
                        logger.Error("Unable to process element of Resume: {0}", element.GetType());
                        continue;
                    }
                    var elementRenderer = elementRenderers[element.GetType()];
                    elementRenderer.Invoke(element);
                }
            }
            document.Close();
            resultBytes = pdfOutputStream.ToArray();
            pdfOutputStream.Close();
            pdfOutputStream.Dispose();
            return new MemoryStream(resultBytes);
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


        public void RenderParagraph(ResumeParagraph resumeParagraph)
        {
            var pdfFont = PdfConverter.GetPdfFont(resumeParagraph.ResumeFont);

            var paragraph = new Paragraph(resumeParagraph.Text, pdfFont)
            {
                Alignment = PdfConverter.TextAlignment(resumeParagraph.HorisontalAlignment),
            };

            this.document.Add(paragraph);
        }



        public void RenderTable(ResumeTable resumeTable)
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


        public void RenderBulletedList(ResumeBulletedList bulletedList)
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