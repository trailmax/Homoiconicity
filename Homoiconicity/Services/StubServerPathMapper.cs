using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Homoiconicity.Services
{
    public interface IServerPathMapper
    {
        String MapPath(String relativePath);
    }


    /// <summary>
    /// Because this sample was not built from the ground up, 
    /// I inherited some service classes that was helping me with the application being an MVC application
    /// and that I had no direct access to the file system... 
    /// 
    /// And this implementation of the interface is actually taken from my test-suite.
    /// My application has different implementation - all it does is Server.MapPath(relativePath).
    /// 
    /// I had to abstract this away for testing purposes. Also I wanted to move this out to a project that has no
    /// reference to MVC libraries.
    /// </summary>
    public class StubServerPathMapper : IServerPathMapper
    {
        public string MapPath(string relativePath)
        {
            var filename = Path.GetFileName(relativePath);
            Debug.Assert(filename != null, "Filename is null!");

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var path = Directory.GetFiles(baseDirectory, filename, SearchOption.AllDirectories);

            if (!path.Any())
            {
                var fileNotFound = String.Format("File not found: {0}", filename);
                throw new ApplicationException(fileNotFound);
            }
            if (path.Count() > 1)
            {
                var manyFilesError = String.Format("Found more than one file: {0}", filename);
                throw new ApplicationException(manyFilesError);
            }

            return path.Single();
        }
    }
}