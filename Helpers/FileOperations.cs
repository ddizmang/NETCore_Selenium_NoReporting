using System.IO;

namespace NETCore_Selenium.Helpers
{
    public class FileOperations
    {
        public bool CheckDirectoryExists(string Path)
        {
            return Directory.Exists(Path) ? true : false;
        }

        public bool CheckFileExists(string Path, string FileName)
        {
            var currentFile = Path + FileName;
            return File.Exists(currentFile) ? true : false;
        }

        public void DeleteFile(string Path, string FileName)
        {
            var currentFile = Path + FileName;
            if (File.Exists(currentFile))
            {
                File.Delete(currentFile);
            }
        }
    }
}
