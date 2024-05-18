using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeMapper
    {
        public static DocumentTypeViewModel ToDocumentTypeViewModel(this DocumentType documentTypeModel)
        {
            return new DocumentTypeViewModel
            {
                Id = documentTypeModel.Id,
                Name = documentTypeModel.Name,
                IsActive = documentTypeModel.IsActive,
                CreatedBy = documentTypeModel.CreatedBy,
                CreatedAt = documentTypeModel.CreatedAt,
                UpdatedBy = documentTypeModel.UpdatedBy,
                UpdatedAt = documentTypeModel.UpdatedAt
            };
        }
        public static CreateDocumentTypeViewModel ToCreateDocumentTypeViewModel(this DocumentType documentTypeModel)
        {
            return new CreateDocumentTypeViewModel
            {
                Name = documentTypeModel.Name,
                IsActive = documentTypeModel.IsActive,
                CreatedBy = documentTypeModel.CreatedBy,
                CreatedAt = documentTypeModel.CreatedAt,
                UpdatedBy = documentTypeModel.UpdatedBy,
                UpdatedAt = documentTypeModel.UpdatedAt
            };
        }
    }

}
