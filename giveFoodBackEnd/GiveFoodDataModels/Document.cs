using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveFoodDataModels
{
    public class Document
    {
        public long Id { get; set; }

        public string StorageProviderId { get; set; }

        [NotMapped]
        public bool IsApproved
        {
            get
            {
                return Status == DocumentStatus.Approved;
            }
        }

        public string Name { get; set; }
                
        public Guid Creator { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
