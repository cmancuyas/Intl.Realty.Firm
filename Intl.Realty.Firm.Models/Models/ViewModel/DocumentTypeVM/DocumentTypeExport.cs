using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM
{
    public class DocumentTypeExport
    {
        public string Code { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
    }
}
