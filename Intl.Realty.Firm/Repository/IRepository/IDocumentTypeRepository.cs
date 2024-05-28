﻿using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {
        Task UpdateAsync(DocumentType obj);
    }
}
