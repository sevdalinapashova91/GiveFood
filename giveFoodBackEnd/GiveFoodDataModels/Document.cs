using System;

namespace GiveFoodDataModels
{
    public class Document
    {
        public long Id { get; set; }

        public string StorageProviderId { get; set; }

        public bool IsApproved { get; set; }

        public string Name { get; set; }
                
        public Guid Creator { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
