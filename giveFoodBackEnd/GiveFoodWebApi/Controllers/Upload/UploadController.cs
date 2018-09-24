using GiveFoodServices.Documents;
using GiveFoodServices.Documents.Models;
using GiveFoodWebApi.Authorization;
using GiveFoodWebApi.Controllers.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Upload
{
    [Authorize]
    public class UploadController : ApplicationController
    {
        private readonly IDocumentService documentService;
        public UploadController(IDocumentService service)
        {
            this.documentService = service;
        }

        [HttpPost]
        public async Task Upload([FromBody]DocumentUploadDto document)
        {
            document.Creator = Guid.Parse(LoggedUserId);
            document.Created = DateTime.UtcNow;
            await this.documentService.UploadAsync(document);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public void Download([FromBody]DocumentViewModel document)
        {
            this.documentService.DownloadAsync(document.Id);
        }
    }
}