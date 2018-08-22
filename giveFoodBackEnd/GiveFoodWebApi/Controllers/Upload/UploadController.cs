using GiveFoodServices.Documents;
using GiveFoodServices.Documents.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Upload
{
    public class UploadController : Controller
    {
        private readonly IDocumentService documentService;
        public UploadController(IDocumentService service)
        {
            this.documentService = service;
        }

        [HttpPost]
        public async Task Upload([FromBody]DocumentUploadDto document)
        {
            await this.documentService.Upload(document);
        }

        [HttpPost]
        public void Download([FromBody]DocumentViewModel document)
        {
            this.documentService.Download(document.Id);
        }
    }
}