using System.Threading.Tasks;
using GiveFoodServices.Documents.Models;

namespace GiveFoodServices.Documents
{
    public interface IDocumentService
    {
        Task Upload(DocumentUploadDto uploadDto);
        Task Download(long documentId);
        Task Approve(long id, bool isApproved);

    }
}