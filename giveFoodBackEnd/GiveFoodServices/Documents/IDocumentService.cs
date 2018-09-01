using System.Threading.Tasks;
using GiveFoodServices.Documents.Models;

namespace GiveFoodServices.Documents
{
    public interface IDocumentService
    {
        Task UploadAsync(DocumentUploadDto uploadDto);
        Task DownloadAsync(long documentId);
        Task ApproveAsync(long id, bool isApproved);
    }
}