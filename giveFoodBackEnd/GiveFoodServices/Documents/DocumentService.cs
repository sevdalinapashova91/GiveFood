using GiveFood.DAL.Documents;
using GiveFoodDataModels;
using GiveFoodServices.Documents.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFoodServices.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly IAmazonService amazonService;
        private readonly IDocumentRepository documentRepo;

        public DocumentService(IAmazonService amazonService, IDocumentRepository documentRepo)
        {
            this.amazonService = amazonService;
            this.documentRepo = documentRepo;
        }

        public async Task UploadAsync(DocumentUploadDto uploadDto)
        {
            var fileId = await amazonService.UploadFileAsync();

            await documentRepo.CreateAsync(fileId, uploadDto.Name, uploadDto.Creator, uploadDto.Created, uploadDto.Status);
        }

        public async Task DownloadAsync(long documentId)
        {
            var document = await documentRepo.GetAsync(documentId);
            var storedDocument = await amazonService.DownloadFileAsync(document.StorageProviderId);
        }

        public IEnumerable<DocumentUploadDto> GetPendingApproval()
        {
            return documentRepo
                .GetAll()
                .Where(x => x.Status == DocumentStatus.PendingApproval)
                .Select(x => new DocumentUploadDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Creator = x.Creator,
                    Created = x.Created,
                    Status = x.Status
                });
        }


        public async Task ApproveAsync(long id, bool isApproved)
        {
            var document = await documentRepo.GetAsync(id);
            var documentStatus = isApproved ? DocumentStatus.Approved : DocumentStatus.NotApproved;
            await documentRepo.UpdateStatusAsync(document, documentStatus);
        }
    }
}
