namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITransactionRepository Transaction { get; set; }
        void Save();
    }
}
