using GiveFoodServices.Documents;
using Microsoft.AspNetCore.Mvc;

namespace GiveFoodWebApi.Controllers.Upload
{
    public class UploadController : Controller
    {
        private readonly IAmazonService amazonService;
        public UploadController(IAmazonService service)
        {
            this.amazonService = service;
        }

        [HttpPost]
        public void StartUpload(DocumentViewModel document)
        {
            this.amazonService.UploadFile(document.DocumentPath);
        }
        public void CompleteUpload() { }
    }
}