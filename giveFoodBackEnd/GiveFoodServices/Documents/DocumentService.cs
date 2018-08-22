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

        public async Task Upload(DocumentUploadDto uploadDto)
        {
            var fileId = await amazonService.UploadFile();

            documentRepo.Create(fileId, uploadDto.Name, uploadDto.Creator, uploadDto.Created, uploadDto.Status);
        }

        public async Task Download(long documentId)
        {
            var document = await documentRepo.Get(documentId);
            var storedDocument = await amazonService.DownloadFile(document.StorageProviderId);
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


        public async Task Approve(long id, bool isApproved)
        {
            var document = await documentRepo.Get(id);
            var documentStatus = isApproved ? DocumentStatus.Approved : DocumentStatus.NotApproved;
            await documentRepo.UpdateStatus(document, documentStatus);
        }
    }
}
