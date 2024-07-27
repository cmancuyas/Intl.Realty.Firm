using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class FileUploadMapper
    {
        public static FileUploadViewModel ToFileUploadViewModel(this FileUpload model)
        {
            return new FileUploadViewModel
            {
                Id = model.Id,
                FileName = model.FileName,
                FullPath = model.FullPath,
                Directory = model.Directory,
                FileExtension = model.FileExtension,
                FileSize = model.FileSize,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static List<FileUpload> ToListFileUploadModel(this List<CreateFileUploadViewModel> viewModelList)
        {
            var modelList = new List<FileUpload>();
            if (viewModelList != null)
            {
                modelList = viewModelList.Select(x => new FileUpload()
                {
                    FileName = x.FileName,
                    FullPath = x.FullPath,
                    Directory = x.Directory,
                    FileExtension = x.FileExtension,
                    FileSize = x.FileSize,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt
                }).ToList();
            }

            return modelList;

        }
        public static CreateFileUploadViewModel ToCreateFileUploadViewModel(this FileUpload model)
        {
            return new CreateFileUploadViewModel
            {
                FileName = model.FileName,
                FullPath = model.FullPath,
                Directory = model.Directory,
                FileExtension = model.FileExtension,
                FileSize = model.FileSize,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static List<FileUploadViewModel> ToFileUploadListViewModel(this List<FileUpload> modelList)
        {
            var viewModelList = new List<FileUploadViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new FileUploadViewModel()
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FullPath = x.FullPath,
                    Directory = x.Directory,
                    FileExtension = x.FileExtension,
                    FileSize = x.FileSize,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<FileUploadViewModel> ToFileUploadListViewModel(this IEnumerable<FileUpload> modelList)
        {
            var viewModelList = new List<FileUploadViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new FileUploadViewModel()
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FullPath = x.FullPath,
                    Directory = x.Directory,
                    FileExtension = x.FileExtension,
                    FileSize = x.FileSize,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<FileUpload> FromIEnumToFileUploadList(this IEnumerable<FileUpload> modelIEnum)
        {
            var modelList = new List<FileUpload>();
            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new FileUpload()
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FullPath = x.FullPath,
                    Directory = x.Directory,
                    FileExtension = x.FileExtension,
                    FileSize = x.FileSize,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return modelList;
        }
    }
}
