﻿using GiveFoodDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFood.DAL.Documents
{
    public interface IDocumentRepository
    {
        Task CreateAsync(string id, string name, Guid creator, DateTime created, DocumentStatus type);

        Task<Document> GetAsync(long id);

        IQueryable<Document> GetAll();

        Task UpdateStatusAsync(Document document, DocumentStatus status);
    }
}
