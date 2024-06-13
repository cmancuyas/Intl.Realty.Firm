namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IIRFDealRepository IRFDeal { get;}
        ITransactionTypeRepository TransactionType { get; }
        IDocumentTypeRepository DocumentType { get; }
        IDocumentTypeAssignmentRepository DocumentTypeAssignment { get; }
        IUserTypeRepository UserType { get; }
        IDepartmentRepository Department { get; }
        IProvinceRepository Province { get; }
        ISaleListingRepository SaleListing { get; }
        ISaleCoopRepository SaleCoop { get; }
        ILeaseListingRepository LeaseListing { get; }
        ILeaseCoopRepository LeaseCoop { get; }
        IFileUploadRepository FileUpload { get; }
        IProfilePictureRepository ProfilePicture { get; }
        void Save();
    }
}
