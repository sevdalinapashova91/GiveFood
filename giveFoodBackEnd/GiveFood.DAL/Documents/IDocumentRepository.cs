using GiveFoodDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFood.DAL.Documents
{
    public interface IDocumentRepository
    {
        Task CreateAsync(string id, string name, Guid creator, DateTime created);

        Task<Document> GetAsync(long id);

        Task<Document> GetByUser(Guid userId);

        IQueryable<Document> GetAll();
        
    }
}
