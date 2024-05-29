﻿using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using System.Xml.Linq;

namespace Intl.Realty.Firm.Repository
{
    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        private ApplicationDbContext _db;
        public DocumentTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(DocumentType model)
        {
            _db.DocumentTypes.Update(model);
            return _db.SaveChangesAsync();
        }

    }
}

