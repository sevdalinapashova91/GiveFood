using GiveFoodDataModels;
using System;

namespace GiveFoodServices.Documents.Models
{
    public class DocumentUploadDto
    {
        public long Id { get; set; }

        public bool IsApproved { get; set; }

        public string Name { get; set; }

        public Guid Creator { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
