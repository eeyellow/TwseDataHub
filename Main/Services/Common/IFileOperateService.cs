using System.IO.Compression;

namespace TwseDataHub.Services.Common
{
    #region FileOperateService
    public interface IFileOperateService
    {
        string ReadTextFile(string path);
        void DeleteDirectory(string path);
        void MoveDirectory(string sourcePath, string destinationPath);
        string SaveAndCalculateChecksum(Stream content, string path, string checksumFilename);
        void ExtractFile(string zipFilePath, string extractPath);
    }
    public class FileOperateService : IFileOperateService
    {
        public string ReadTextFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return null;
        }

        public void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public void MoveDirectory(string sourcePath, string destinationPath)
        {
            if (Directory.Exists(sourcePath))
            {
                Directory.Move(sourcePath, destinationPath);
            }
        }
        
        public string SaveAndCalculateChecksum(Stream content, string path, string checksumFilename)
        {
            var newChecksumText = "";
            var dirPath = Path.GetDirectoryName($"{path}");
            Directory.CreateDirectory(dirPath);
            if (File.Exists(path)) 
            { 
                File.Delete(path); 
            }
            using (var fs = new FileStream(path, FileMode.CreateNew))
            {
                content.CopyToAsync(fs).Wait();

                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    var hash = md5.ComputeHash(fs);
                    newChecksumText = BitConverter.ToString(hash);
                    File.WriteAllText($"{dirPath}/{checksumFilename}", newChecksumText);
                }
            }

            return newChecksumText;
        }

        public void ExtractFile(string zipFilePath, string extractPath)
        {
            using (ZipArchive archive = new ZipArchive(File.OpenRead(zipFilePath), ZipArchiveMode.Read, false, Encoding.UTF8))
            {
                archive.ExtractToDirectory(extractPath, true);
            }
        }
    }
    #endregion FileOperateService
}
