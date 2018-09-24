using GiveFood.DAL.Documents;
using GiveFoodDataModels;
using GiveFoodServices.Documents.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            var fileId = await amazonService.UploadFileAsync(uploadDto.Name);

            await documentRepo.CreateAsync(fileId, uploadDto.Name, uploadDto.Creator, uploadDto.Created);
        }

        public async Task DownloadAsync(long documentId)
        {
            var document = await documentRepo.GetAsync(documentId);
            var storedDocument = await amazonService.DownloadFileAsync(document.StorageProviderId);
        }

        public Task<Document> GetDocumentByUser(Guid userId)
        => this.documentRepo.GetByUser(userId);
    }
}
