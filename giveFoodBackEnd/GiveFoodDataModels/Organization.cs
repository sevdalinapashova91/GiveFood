using System;
using System.Collections.Generic;

namespace GiveFoodDataModels
{
    public class Organization
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public ICollection<Document> AcceptanceDocuments { get; set; }
        
        public  OrganizationType Type { get; set; }
        public Document Logo { get; set; }
        public Status Status { get; set; }
    }
}
