using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class FileUploadMapper
    {
        public static FileUploadViewModel ToFileUploadViewModel(this FileUpload model)
        {
            return new FileUploadViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
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
        public static FileUpload ToFileUploadModel(this CreateFileUploadViewModel viewModel)
        {
            return new FileUpload
            {
                TransactionTypeId = viewModel.TransactionTypeId,
                IRFDealId = viewModel.IRFDealId,
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
        public static CreateFileUploadViewModel ToCreateFileUploadViewModel(this FileUpload model)
        {
            return new CreateFileUploadViewModel
            {
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
                FileName = model.FileName,
                FilePath = model.FilePath,
                FileType = model.FileType,
                FileSize = model.FileSize,
                WebDirectoryPath = model.WebDirectoryPath,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditFileUploadViewModel ToEditFileUploadModel(this FileUpload model)
        {
            return new EditFileUploadViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
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
        public static EditFileUploadViewModel ToEditFileUploadViewModel(this FileUpload model)
        {
            return new EditFileUploadViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
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
        public static List<FileUploadViewModel> ToFileUploadListViewModel(this List<FileUpload> modelList)
        {
            var viewModelList = new List<FileUploadViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new FileUploadViewModel()
                {
                    Id = x.Id,
                    TransactionTypeId = x.TransactionTypeId,
                    IRFDealId = x.IRFDealId,
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    FileType = x.FileType,
                    FileSize = x.FileSize,
                    WebDirectoryPath = x.WebDirectoryPath,
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
                    TransactionTypeId = x.TransactionTypeId,
                    IRFDealId = x.IRFDealId,
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    FileType = x.FileType,
                    FileSize = x.FileSize,
                    WebDirectoryPath = x.WebDirectoryPath,
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
                    TransactionTypeId = x.TransactionTypeId,
                    IRFDealId = x.IRFDealId,
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    FileType = x.FileType,
                    FileSize = x.FileSize,
                    WebDirectoryPath = x.WebDirectoryPath,
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
