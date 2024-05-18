namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IIRFDealRepository IRFDeal { get;}
        ITransactionTypeRepository TransactionType { get; }
        IDocumentTypeRepository DocumentType { get; }
        void Save();
    }
}
