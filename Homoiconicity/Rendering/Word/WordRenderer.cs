using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Homoiconicity.Data;
using Homoiconicity.Elements;
using Homoiconicity.Sections;
using Homoiconicity.Services;

namespace Homoiconicity.Rendering.Word
{
    public class WordRenderer : IRenderer
    {
        private readonly List<OpenXmlElement> abstractNumberingInstances;
        private readonly List<OpenXmlElement> numberingInstances;
        private readonly ILoggingService logger;

        private Body body;
        private MainDocumentPart mainDocumentPart;
        private StyleDefinitionsPart styleDefinitionsPart;
        private NumberingDefinitionsPart numberingDefinitionsPart;


        public WordRenderer(ILoggingService logger)
        {
            this.logger = logger;
            abstractNumberingInstances = new List<OpenXmlElement>();
            numberingInstances = new List<OpenXmlElement>();
        }


        public override MemoryStream CreateDocument(IEnumerable<IResumeSection> resumeSections, ResumeData data)
        {
            var memoryStream = new MemoryStream();
            using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                mainDocumentPart = wordDocument.AddMainDocumentPart();

                // set document settings
                var documentSettingsPart = mainDocumentPart.AddNewPart<DocumentSettingsPart>();
                documentSettingsPart.Settings = new Settings(new EvenAndOddHeaders());


                // set headers and footers
                var wordBrandingHelper = new WordBranding(mainDocumentPart);
                wordBrandingHelper.AddHeadersAndFooters();

                styleDefinitionsPart = mainDocumentPart.AddNewPart<StyleDefinitionsPart>();
                GenerateStyleDefinitions(styleDefinitionsPart);


                numberingDefinitionsPart = mainDocumentPart.AddNewPart<NumberingDefinitionsPart>();
                numberingDefinitionsPart.Numbering = new Numbering();


                RenderSections(resumeSections, data);

                // apply sections to the document - sections must describe what headers and footers are present
                body.AppendChild(wordBrandingHelper.CreateSections());

                // add numbering instances to the numbering sections. Must be all abstract first, followed by all numbering instances.
                // hence it is moved out up here.
                numberingDefinitionsPart.Numbering.Append(abstractNumberingInstances);
                numberingDefinitionsPart.Numbering.Append(numberingInstances);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }


        private void RenderSections(IEnumerable<IResumeSection> resumeSections, ResumeData data)
        {
            mainDocumentPart.Document = new Document();
            body = mainDocumentPart.Document.AppendChild(new Body());

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
        }


        protected override void RenderParagraph(ResumeParagraph resumeParagraph)
        {
            var wordParagraph = WordConverter.GetWordParagraph(resumeParagraph);

            this.body.AppendChild(wordParagraph);
        }


        protected override void RenderTable(ResumeTable resumeTable)
        {
            var wordTable = new Table();

            var tableProperties = wordTable.AppendChild(new TableProperties());

            tableProperties.AppendChild(new TableWidth() { Width = ConvertToPctWidth(resumeTable.WidthPercentage), Type = TableWidthUnitValues.Pct });
            tableProperties.AppendChild(WordConverter.GetJustification(resumeTable.HorisontalAlignment));

            // need this table grid for validation purposes. Otherwise the validation by schema is failed
            var tableGrid = wordTable.AppendChild(new TableGrid());

            for (var i = 0; i < resumeTable.RelativeColumnWidths.Count(); i++)
            {
                // for each column we need to add a GridColumn. 
                // No idea why we need this, but without this validation fails
                tableGrid.AppendChild(new GridColumn());
            }

            foreach (var resumeRow in resumeTable)
            {
                var wordRow = wordTable.AppendChild(new TableRow());

                for (var i = 0; i < resumeRow.Count; i++)
                {
                    var resumeCell = resumeRow[i];
                    var wordCell = wordRow.AppendChild(new TableCell());
                    var tableCellProperties = new TableCellProperties(
                        new TableCellWidth()
                            {
                                Width = ConvertToPctWidth(resumeTable.RelativeColumnWidths[i]),
                                Type = TableWidthUnitValues.Pct
                            });

                    wordCell.AppendChild(tableCellProperties);
                    wordCell.AppendChild(WordConverter.GetWordParagraph(resumeCell.Paragraph));
                }
            }

            this.body.AppendChild(wordTable);
        }


        private int numberberingId = 1;
        protected override void RenderBulletedList(ResumeBulletedList bulletedList)
        {
            var currentNumberingId = numberberingId++;
            GenerateNumberingDefinitions(currentNumberingId);

            foreach (var listItem in bulletedList.Paragraphs)
            {
                var run = new Run();
                run.AppendChild(WordConverter.GetRunProperties(listItem.ResumeFont));
                run.Append(WordConverter.CreateLineBreaks(listItem.Text));

                var extraProperties = new List<OpenXmlElement>()
                                      {
                                          new ParagraphStyleId() { Val = "ListParagraph" },
                                          new NumberingProperties(
                                              new NumberingLevelReference() { Val = 0 },
                                              new NumberingId() { Val = currentNumberingId })
                                      };

                var paragraphProperties = WordConverter.GetParagraphProperties(listItem, extraProperties);

                var wordParagraph = new Paragraph(paragraphProperties, run);

                this.body.AppendChild(wordParagraph);
            }
        }

        private void GenerateNumberingDefinitions(int numberId)
        {
            var level = new Level()
            {
                LevelIndex = 0,
                TemplateCode = "18FCCCF6",
            };

            var numberingSymbolRunProperties = new NumberingSymbolRunProperties();
            numberingSymbolRunProperties.AppendChild(new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" });

            level.AppendChild(new StartNumberingValue() { Val = 1 });
            level.AppendChild(new NumberingFormat() { Val = NumberFormatValues.Bullet });
            level.AppendChild(new LevelText() { Val = "·" });
            level.AppendChild(new LevelJustification() { Val = LevelJustificationValues.Left });
            level.AppendChild(new PreviousParagraphProperties(new Indentation() { Left = "720", Hanging = "360" }));
            level.AppendChild(numberingSymbolRunProperties);

            abstractNumberingInstances.Add(new AbstractNum(level) { AbstractNumberId = numberId });
            numberingInstances.Add(new NumberingInstance(new AbstractNumId() { Val = numberId }) { NumberID = numberId });
        }



        private void GenerateStyleDefinitions(StyleDefinitionsPart definitionsPart)
        {
            definitionsPart.Styles = new Styles();

            var style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = "ListParagraph"
            };

            var styleParagraphProperties = new StyleParagraphProperties(
                new Indentation() { Left = "720" },
                new ContextualSpacing());


            style.AppendChild(new StyleName() { Val = "List Paragraph" });
            style.AppendChild(styleParagraphProperties);

            styleDefinitionsPart.Styles.AppendChild(style);
        }


        private static string ConvertToPctWidth(float widthPercentage)
        {
            // convert width from percent to Pct. Max Pct is 5000, so need to multiply percentage by 50
            return ((int)widthPercentage * 50).ToString(CultureInfo.InvariantCulture);
        }
    }
}
