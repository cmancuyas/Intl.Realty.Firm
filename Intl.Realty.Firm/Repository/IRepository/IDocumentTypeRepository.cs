using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {
        void Update(DocumentType obj);
    }
}
