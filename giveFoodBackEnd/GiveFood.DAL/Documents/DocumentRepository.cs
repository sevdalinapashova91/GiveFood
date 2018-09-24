using System;
using GiveFoodDataModels;
using GiveFoodData;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GiveFood.DAL.Documents
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly GiveFoodDbContext dbContext;

        public DocumentRepository(GiveFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task CreateAsync(string storageProviderId, string name, Guid creator, DateTime created)
        {
            var document = new Document
            {
                StorageProviderId = storageProviderId,
                Name = name,
                Creator = creator,
                Created = created
            };

            dbContext.Add(document);

            return dbContext.SaveChangesAsync();
        }

        public async Task<Document> GetAsync(long id) => await dbContext.Documents.FindAsync(id);

        public IQueryable<Document> GetAll() => dbContext.Documents;
      

        public Task<Document> GetByUser(Guid userId)  =>
            this.dbContext.Documents.FirstOrDefaultAsync(x => x.Creator == userId);
        
    }
}
