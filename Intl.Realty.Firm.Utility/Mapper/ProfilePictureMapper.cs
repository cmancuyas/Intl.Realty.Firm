using Intl.Realty.Firm.Models.Models.ViewModel.ProfilePictureVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class ProfilePictureMapper
    {
        public static List<ProfilePictureViewModel> ToProfilePictureListViewModel(this IEnumerable<ProfilePicture> modelList)
        {
            List<ProfilePictureViewModel> viewModelList = new List<ProfilePictureViewModel>();

            if (modelList != null)
            {
                viewModelList = modelList.Select(o => new ProfilePictureViewModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    FileName = o.FileName,
                    FilePath = o.FilePath,
                    FileType = o.FileType,
                    FileSize = o.FileSize,
                    WebDirectoryPath = o.WebDirectoryPath,
                    IsActive = o.IsActive,
                    CreatedBy = o.CreatedBy,
                    CreatedAt = o.CreatedAt,
                    UpdatedBy = o.UpdatedBy,
                    UpdatedAt = o.UpdatedAt,
                }).ToList();
            }

            return viewModelList;
        }
        public static ProfilePictureViewModel ToProfilePictureViewModel(this ProfilePicture model)
        {
            return new ProfilePictureViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                FileName = model.FileName,
                FilePath = model.FilePath,
                FileType = model.FileType,
                FileSize = model.FileSize,
                WebDirectoryPath = model.WebDirectoryPath,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static ProfilePicture ToProfilePictureModel(this CreateProfilePictureViewModel viewModel)
        {
            return new ProfilePicture
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                FileName = viewModel.FileName,
                FilePath = viewModel.FilePath,
                FileType = viewModel.FileType,
                FileSize = viewModel.FileSize,
                WebDirectoryPath = viewModel.WebDirectoryPath,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt
            };
        }

        public static ProfilePicture ToProfilePictureModel(this ProfilePictureViewModel viewModel)
        {
            return new ProfilePicture
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                FileName = viewModel.FileName,
                FilePath = viewModel.FilePath,
                FileType = viewModel.FileType,
                FileSize = viewModel.FileSize,
                WebDirectoryPath = viewModel.WebDirectoryPath,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static EditProfilePictureViewModel ToEditProfilePictureModel(this ProfilePicture model)
        {
            return new EditProfilePictureViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                FileName = model.FileName,
                FilePath = model.FilePath,
                FileType = model.FileType,
                FileSize = model.FileSize,
                WebDirectoryPath = model.WebDirectoryPath,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
