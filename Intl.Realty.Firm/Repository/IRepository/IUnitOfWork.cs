namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get;}
        IIRFDealRepository IRFDeal { get;}
        ITransactionTypeRepository TransactionType { get; }
        IDocumentTypeRepository DocumentType { get; }
        IDocumentTypeAssignmentRepository DocumentTypeAssignment { get; }
        IUserTypeRepository UserType { get; }
        IEmploymentStatusRepository EmploymentStatus { get; }
        IRoleRepository Role { get; }
        IRolePermissionRepository RolePermission { get; }
        IPermissionRepository Permission { get; }
        IDepartmentRepository Department { get; }
        IProvinceRepository Province { get; }
        ISaleListingRepository SaleListing { get; }
        ISaleCoopRepository SaleCoop { get; }
        ILeaseListingRepository LeaseListing { get; }
        ILeaseCoopRepository LeaseCoop { get; }
        IFileUploadRepository FileUpload { get; }
        IProfilePictureRepository ProfilePicture { get; }
        IFileDocumentTypeBridgeRepository FileDocumentTypeBridge { get; }
        IActivityLogRepository ActivityLog { get; }
        void Save();
    }
}
