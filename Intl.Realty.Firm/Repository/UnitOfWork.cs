using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public IIRFDealRepository IRFDeal { get; private set; }
        public ITransactionTypeRepository TransactionType { get; private set; }
        public IDocumentTypeRepository DocumentType { get; private set; }
        public IDocumentTypeAssignmentRepository DocumentTypeAssignment { get; private set; }
        public IEmploymentStatusRepository EmploymentStatus { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IRoleRepository Role { get; private set; }
        public IRolePermissionRepository RolePermission { get; private set; }
        public IPermissionRepository Permission { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IProvinceRepository Province { get; private set; }
        public ISaleListingRepository SaleListing { get; private set; }
        public ISaleCoopRepository SaleCoop { get; private set; }
        public ILeaseListingRepository LeaseListing { get; private set; }
        public ILeaseCoopRepository LeaseCoop { get; private set; }
        public IFileUploadRepository FileUpload { get; private set; }
        public IDealStatusRepository DealStatus { get; private set; }
        public IProfilePictureRepository ProfilePicture { get; private set; }
        public IFileDocumentTypeBridgeRepository FileDocumentTypeBridge { get; private set; }
        public IActivityLogRepository ActivityLog { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Customer = new CustomerRepository(_db);
            IRFDeal = new IRFDealRepository(_db);
            TransactionType = new TransactionTypeRepository(_db);
            DocumentType = new DocumentTypeRepository(_db);
            DocumentTypeAssignment = new DocumentTypeAssignmentRepository(_db);
            EmploymentStatus = new EmploymentStatusRepository(_db);
            Role = new RoleRepository(_db);
            RolePermission = new RolePermissionRepository(_db);
            Permission = new PermissionRepository(_db);
            Department = new DepartmentRepository(_db);
            Province = new ProvinceRepository(_db);
            SaleListing = new SaleListingRepository(_db);
            SaleCoop = new SaleCoopRepository(_db);
            LeaseListing = new LeaseListingRepository(_db);
            LeaseCoop = new LeaseCoopRepository(_db);
            ProfilePicture = new ProfilePictureRepository(_db);
            FileUpload = new FileUploadRepository(_db);
            DealStatus = new DealStatusRepository(_db);
            FileDocumentTypeBridge = new FileDocumentTypeBridgeRepository(_db);
            ActivityLog = new ActivityLogRepository(_db);
        }

        public void SaveAsync()
        {
            _db.SaveChangesAsync();
        }
    }
}
