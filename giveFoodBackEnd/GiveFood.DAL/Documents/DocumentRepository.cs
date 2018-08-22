﻿using System;
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

        public void Create(string storageProviderId, string name, Guid creator, DateTime created, DocumentStatus type)
        {
            var document = new Document
            {
                StorageProviderId = storageProviderId,
                Name = name,
                Creator = creator,
                Status = type,
                Created = created
            };

            dbContext.Add(document);

            dbContext.SaveChangesAsync();
        }

        public async Task<Document> Get(long id) => await dbContext.Documents.FindAsync(id);

        public IQueryable<Document> GetAll() =>  dbContext.Documents;

        public Task UpdateStatus(Document document, DocumentStatus status)
        {
            document.Status = status;
            return dbContext.SaveChangesAsync();
        }
    }
}