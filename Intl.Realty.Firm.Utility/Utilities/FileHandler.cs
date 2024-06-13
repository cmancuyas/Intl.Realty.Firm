using Microsoft.AspNetCore.Http;

namespace DENR_FAPIS.Utilities
{
    public static class FileHandler
    {
        public static void WriteFile(IFormFile file, string directoryPath, string fileNameWithExtension)
        {
            // IFormFile file - contains the file to be written
            // string directoryPath - the path of the folder where the file will be saved (File name and extension are not included)
            // string fileNameWithExtension - file name with extension (Ex. "MyFile.pdf")

            // CREATE FOLDER IF PATH DOESN'T EXIST
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string fullFilePath = Path.Combine(directoryPath, fileNameWithExtension);

            // SAVE THE FILE
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        
        public static void DeleteFile(string filePathFull)
        {
            // filePathFull - File Path Containing the filename (Ex. "C:\Users\Documents\MyFile.docx")

            // Delete Old Profile Pic on Folder
            if (System.IO.File.Exists(filePathFull))
            {
                System.IO.File.Delete(filePathFull);
            }
        }
    }
}
