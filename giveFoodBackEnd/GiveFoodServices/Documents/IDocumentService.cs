using System.Threading.Tasks;
using GiveFoodServices.Documents.Models;
using GiveFoodDataModels;
using System;

namespace GiveFoodServices.Documents
{
    public interface IDocumentService
    {
        Task UploadAsync(DocumentUploadDto uploadDto);
        Task DownloadAsync(long documentId);
        Task<Document> GetDocumentByUser(Guid userId);
    }
}