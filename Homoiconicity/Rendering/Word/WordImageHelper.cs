using System;
using System.Drawing;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Path = System.IO.Path;

namespace Homoiconicity.Rendering.Word
{
    public static class WordImageHelper
    {
        public static OpenXmlElement InsertImage(OpenXmlPart mainPart, string pathToImage)
        {
            var attachedImage = AttachImage(mainPart, pathToImage);
            var element = GetImageElement(attachedImage);

            return element;
        }

        private static AttachedImage AttachImage(OpenXmlPart mainPart, string pathToImage)
        {
            var imagePartType = DeterminImagePartType(pathToImage);

            var imagePart = AddImagePart(mainPart, imagePartType);

            using (var stream = File.OpenRead(pathToImage))
            {
                imagePart.FeedData(stream);
            }

            var result = new AttachedImage()
            {
                PartId = mainPart.GetIdOfPart(imagePart),
                WidthEmu = 0,
                HeightEmu = 0,
            };

            var imageFile = new Bitmap(pathToImage);
            result.WidthEmu = (long)((imageFile.Width / imageFile.HorizontalResolution) * 914400L);
            result.HeightEmu = (long)((imageFile.Height / imageFile.VerticalResolution) * 914400L);

            return result;
        }


        private static ImagePart AddImagePart(OpenXmlPart documentPart, ImagePartType imagePartType)
        {
            var mainPart = documentPart as MainDocumentPart;
            if (mainPart != null)
            {
                return mainPart.AddImagePart(imagePartType);
            }
            var headerPart = documentPart as HeaderPart;
            if (headerPart != null)
            {
                return headerPart.AddImagePart(imagePartType);
            }

            var footerPart = documentPart as FooterPart;
            if (footerPart != null)
            {
                return footerPart.AddImagePart(imagePartType);
            }

            throw new ArgumentException("'documentPart' is of type not supported by this function");
        }


        private static OpenXmlElement GetImageElement(AttachedImage attachedImage)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DocumentFormat.OpenXml.Drawing.Wordprocessing.Inline(
                         new DocumentFormat.OpenXml.Drawing.Wordprocessing.Extent()
                         {
                             Cx = attachedImage.WidthEmu,
                             Cy = attachedImage.HeightEmu,
                         },
                         new DocumentFormat.OpenXml.Drawing.Wordprocessing.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DocumentFormat.OpenXml.Drawing.Wordprocessing.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = ""
                         },
                         new DocumentFormat.OpenXml.Drawing.Wordprocessing.NonVisualGraphicFrameDrawingProperties(
                             new DocumentFormat.OpenXml.Drawing.GraphicFrameLocks() { NoChangeAspect = true }),
                         new DocumentFormat.OpenXml.Drawing.Graphic(
                             new DocumentFormat.OpenXml.Drawing.GraphicData(
                                 new DocumentFormat.OpenXml.Drawing.Pictures.Picture(
                                     new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureProperties(
                                         new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = ""
                                         },
                                         new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureDrawingProperties()),
                                     new DocumentFormat.OpenXml.Drawing.Pictures.BlipFill(
                                         new DocumentFormat.OpenXml.Drawing.Blip(
                                             new DocumentFormat.OpenXml.Drawing.BlipExtensionList(
                                                 new DocumentFormat.OpenXml.Drawing.BlipExtension()
                                                 {
                                                     Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 }))
                                         {
                                             Embed = attachedImage.PartId,
                                             CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print
                                         },
                                         new DocumentFormat.OpenXml.Drawing.Stretch(
                                             new DocumentFormat.OpenXml.Drawing.FillRectangle())),
                                     new DocumentFormat.OpenXml.Drawing.Pictures.ShapeProperties(
                                         new DocumentFormat.OpenXml.Drawing.Transform2D(
                                             new DocumentFormat.OpenXml.Drawing.Offset() { X = 0L, Y = 0L },
                                             new DocumentFormat.OpenXml.Drawing.Extents()
                                             {
                                                 Cx = attachedImage.WidthEmu,
                                                 Cy = attachedImage.HeightEmu,
                                             }),
                                         new DocumentFormat.OpenXml.Drawing.PresetGeometry(new DocumentFormat.OpenXml.Drawing.AdjustValueList()) { Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle })))
                                 {
                                     Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                                 }))
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                     });

            return element;
        }


        private struct AttachedImage
        {
            public long WidthEmu;
            public long HeightEmu;
            public string PartId;
        }


        private static ImagePartType DeterminImagePartType(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            ImagePartType imagePartType;
            switch (extension)
            {
                case ".gif":
                    imagePartType = ImagePartType.Gif;
                    break;
                case ".png":
                    imagePartType = ImagePartType.Png;
                    break;
                case ".jpeg":
                    imagePartType = ImagePartType.Jpeg;
                    break;
                case ".jpg":
                    imagePartType = ImagePartType.Jpeg;
                    break;
                default:
                    imagePartType = ImagePartType.Jpeg;
                    break;
            }
            return imagePartType;
        }
    }
}