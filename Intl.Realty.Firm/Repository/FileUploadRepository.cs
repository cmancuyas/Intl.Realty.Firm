using DENR_FAPIS.Utilities;
using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.StaticFiles;

namespace Intl.Realty.Firm.Repository
{
    public class FileUploadRepository : Repository<FileUpload>, IFileUploadRepository
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public FileUploadRepository(ApplicationDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
            _configuration = configuration;
        }

        public Task UpdateAsync(FileUpload model)
        {
            _db.FileUploads.Update(model);
            return _db.SaveChangesAsync();
        }
        public bool DeleteFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<(byte[], string, string)> DownloadFile(string directory, string fileName, string fileExtension)
        {
            try
            {
                var filePath = FileHandler.GetFilePath(directory, fileName + fileExtension);
                filePath = StringManipulation.ReplaceWhitespace(filePath, "");
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var readAllBytesAsync = await File.ReadAllBytesAsync(filePath);
                return (readAllBytesAsync, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string, string)> UploadFile(IFormFile iFormFile, string directory)
        {
            string fileNameWithoutExtension = string.Empty;
            string fileExtension = string.Empty;

            try
            {
                if (iFormFile != null && iFormFile?.Length > 0)
                {
                    (fileNameWithoutExtension, fileExtension) = await UploadFileToDirectory(iFormFile, directory);
                }
                return (fileNameWithoutExtension, fileExtension);
            }
            catch (Exception ex)
            {
                throw new Exception("File could not be uploaded", ex);
            }
        }
        public async Task<(string, string)> UploadFileToDirectory(IFormFile iFormFile, string directory)
        {
            string fileNameWithoutExtension = string.Empty;
            string fileExtension = string.Empty;

            FileInfo fileInfo = new FileInfo(iFormFile.FileName);

            fileExtension = fileInfo.Extension;

            var fileName = iFormFile.FileName;
            fileNameWithoutExtension = FileHandler.GetFileNameWithoutExtension(fileName);
            var fullFileName = fileNameWithoutExtension + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
            fileNameWithoutExtension = FileHandler.GetFileNameWithoutExtension(fullFileName);

            var filePath = FileHandler.GetFilePath(directory, fullFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await iFormFile.CopyToAsync(fileStream);
            }
            return (fileNameWithoutExtension, fileExtension);
        }

        public string GetDefaultUploadPathFromConfig()
        {
            var defaultPath = _configuration["FileUpload:DefaultPath"];
            if (defaultPath != null)
            {
                return defaultPath;
            }
            return "";
        }
    }
}