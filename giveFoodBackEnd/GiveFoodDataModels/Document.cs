using System;

namespace GiveFoodDataModels
{
    public class Document
    {
        public bool Current { get; set; }
        public string Name { get; set; }
        public Guid Creater { get; set; }
        public DateTime Created { get; set; }
        public long Size { get; set; }
        public DocumentType Type { get; set; }
    }
}
