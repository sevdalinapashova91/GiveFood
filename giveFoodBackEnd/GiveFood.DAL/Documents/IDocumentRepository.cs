using GiveFoodDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFood.DAL.Documents
{
    public interface IDocumentRepository
    {
        void Create(string id, string name, Guid creator, DateTime created, DocumentStatus type);

        Task<Document> Get(long id);

        IQueryable<Document> GetAll();

        Task UpdateStatus(Document document, DocumentStatus status);
    }
}
