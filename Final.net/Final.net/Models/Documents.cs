using System;
using System.Collections.Generic;

namespace Final.net.Models
{
    public partial class Documents
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public int? Size { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FolderId { get; set; }
        public bool? IsDeleted { get; set; }

        public Users CreatedByNavigation { get; set; }
        public Folders Folder { get; set; }
    }
}
