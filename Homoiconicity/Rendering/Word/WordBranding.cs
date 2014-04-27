using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Homoiconicity.Rendering.Word
{
    public class WordBranding
    {
        public const string FirstHeader = "rId2";
        public const string OddHeader = "rId3";
        public const string EvenHeader = "rId4";
        public const string FirstFooter = "rId5";
        public const string OddFooter = "rId6";
        public const string EvenFooter = "rId7";


        private readonly MainDocumentPart mainPart;



        public WordBranding(MainDocumentPart mainPart)
        {
            this.mainPart = mainPart;
        }


        public void AddHeadersAndFooters()
        {
            var firstPageHeaderPart = mainPart.AddNewPart<HeaderPart>(FirstHeader);
            GenerateFirstPageHeaderPart(firstPageHeaderPart);

            var oddPageHeaderPart = mainPart.AddNewPart<HeaderPart>(OddHeader);
            GenerateDefaultHeaderPart(oddPageHeaderPart);

            var evenPageHeaderPart = mainPart.AddNewPart<HeaderPart>(EvenHeader);
            GenerateDefaultHeaderPart(evenPageHeaderPart);

            var firstFooterPart = mainPart.AddNewPart<FooterPart>(FirstFooter);
            GenerateDefaultFooterPart(firstFooterPart);

            var oddFooterPart = mainPart.AddNewPart<FooterPart>(OddFooter);
            GenerateDefaultFooterPart(oddFooterPart);

            var evenFooterPart = mainPart.AddNewPart<FooterPart>(EvenFooter);
            GenerateDefaultFooterPart(evenFooterPart);
        }


        private void GenerateFirstPageHeaderPart(HeaderPart firstPageHeaderPart)
        {
            var paragraph = new Paragraph(new Run(new Text("First Header")));

            firstPageHeaderPart.Header = new Header(paragraph);
        }


        private void GenerateDefaultHeaderPart(HeaderPart headerPart)
        {
            var paragraph = new Paragraph(new Run(new Text("Default Header")));

            headerPart.Header = new Header(paragraph);
        }



        private void GenerateDefaultFooterPart(FooterPart footerPart)
        {
            var paragraph = new Paragraph(new Run(PageNumberField()));

            footerPart.Footer = new Footer(paragraph);
        }


        private static SimpleField PageNumberField()
        {
            var simpleField1 = new SimpleField() { Instruction = " PAGE   \\* MERGEFORMAT " };

            simpleField1.AppendChild(
                new Run(
                    new RunProperties(new NoProof()), 
                    new Text("1")));

            return simpleField1;
        }


        public SectionProperties CreateSections()
        {
            return new SectionProperties(
                new HeaderReference()
                {
                    Type = HeaderFooterValues.First,
                    Id = FirstHeader,
                },
                new HeaderReference()
                {
                    Type = HeaderFooterValues.Default,
                    Id = OddHeader,
                },
                new HeaderReference()
                {
                    Type = HeaderFooterValues.Even,
                    Id = EvenHeader,
                },
                new FooterReference()
                {
                    Type = HeaderFooterValues.First,
                    Id = FirstFooter
                },
                new FooterReference()
                {
                    Type = HeaderFooterValues.Even,
                    Id = EvenFooter
                },
                new FooterReference()
                {
                    Type = HeaderFooterValues.Default,
                    Id = OddFooter
                },
                new PageMargin()
                {
                    //Top = (Int32Value)20,
                    //Right = (UInt32Value)20,
                    //Bottom = (Int32Value)20,
                    //Left = (UInt32Value)20,

                    //Header = (UInt32Value)720UL,
                    //Footer = (UInt32Value)720UL,
                    //Gutter = (UInt32Value)0UL
                },
                new TitlePage());
        }
    }
}