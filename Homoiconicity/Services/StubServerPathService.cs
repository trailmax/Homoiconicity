using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Homoiconicity.Services
{
    public class StubServerPathService : IServerPathService
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