using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Homoiconicity.Data;
using Homoiconicity.Rendering.Pdf;
using Homoiconicity.Rendering.Word;
using Homoiconicity.Sections;
using Homoiconicity.Services;
using Newtonsoft.Json;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var resumeSections = ResumeComposer.ComposeBasicResume();
            var resumeSections = ResumeComposer.ComposeResumeForTopSecretAgents();

            ResumeSectionsToBinaryFormat(resumeSections, "ResumeSections.cv");

            var sectionsFromBinary = ReadResumeSectionsFromBinary("ResumeSections.cv");


            var json = SerializeAsJson(sectionsFromBinary);

            var sectionsFromJson = DeserializeJson(json);


            var resumeData = Data.JamesBond;
            CreatePdf(sectionsFromJson, resumeData);

            CreateWord(sectionsFromJson, resumeData);
        }


        private static void ResumeSectionsToBinaryFormat(List<IResumeSection> resumeSections, String fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, resumeSections);
                
            }
        }


        private static List<IResumeSection> ReadResumeSectionsFromBinary(String fileName)
        {
            using (var deStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var formatter = new BinaryFormatter();

                var sections = (List<IResumeSection>)formatter.Deserialize(deStream);
                return sections;
            }
        }


        private static String SerializeAsJson(IEnumerable<IResumeSection> resumeSections)
        {
            var jsonSerializerOptions = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            var json = JsonConvert.SerializeObject(resumeSections, jsonSerializerOptions);

            return json;
        }


        private static List<IResumeSection> DeserializeJson(String json)
        {
            var jsonSerializerOptions = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            var resumeSections = JsonConvert.DeserializeObject<List<IResumeSection>>(json, jsonSerializerOptions);

            return resumeSections;
        }


        private static void CreatePdf(IEnumerable<IResumeSection> basicResumeComposer, ResumeData resumeData)
        {
            var pdfRenderer = new PdfRenderer(new TraceLogger(), new StubServerPathService());

            var pdfStream = pdfRenderer.CreateDocument(basicResumeComposer, resumeData);

            File.WriteAllBytes("pdfResume.pdf", pdfStream.ToArray());
        }


        private static void CreateWord(IEnumerable<IResumeSection> basicResumeComposer, ResumeData resumeData)
        {
            var wordRenderer = new WordRenderer(new TraceLogger());

            var wordStream = wordRenderer.CreateDocument(basicResumeComposer, resumeData);

            File.WriteAllBytes("wordResume.docx", wordStream.ToArray());
        }
    }
}
