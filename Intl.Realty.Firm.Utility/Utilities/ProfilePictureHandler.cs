using Intl.Realty.Firm.Models.Models;
using Microsoft.AspNetCore.Http;

namespace DENR_FAPIS.Utilities
{
    public class ProfilePictureHandler
    {
        public static ProfilePicture SaveProfilePicture(IFormFile profilePhoto, int userId)
        {
            if (profilePhoto == null)
                throw new ArgumentNullException(nameof(profilePhoto));

            // File Info
            FileInfo fileInfo = new FileInfo(profilePhoto.FileName);

            // Set the folder and file names
            string folderName = "USER-" + userId;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePhoto.FileName);
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\User-Files\\Profile-Pictures\\" + folderName);
            string webDirectoryPath = "/Files/User-Files/Profile-Pictures/" + folderName;

            // Ensure the directory exists
            Directory.CreateDirectory(directoryPath);

            // Save the file
            FileHandler.WriteFile(profilePhoto, directoryPath, fileName);

            // Create and return the profile picture model
            var profilePictureModel = new ProfilePicture
            {
                UserId = userId,
                FileName = fileName,
                FileType = fileInfo.Extension,
                FilePath = directoryPath,
                WebDirectoryPath = webDirectoryPath,
                FileSize = profilePhoto.Length.ToString(),
                IsActive = true,
                // CreatedBy and CreatedAt can be set in the service layer
                UpdatedBy = userId,
                UpdatedAt = DateTime.Now
            };

            return profilePictureModel;
        }
    }
}
