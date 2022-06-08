using System;
using System.Collections.Generic;

namespace Final.net.Models
{
    public partial class Folders
    {
        public Folders()
        {
            Documents = new HashSet<Documents>();
        }

        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public Users CreatedByNavigation { get; set; }
        public ICollection<Documents> Documents { get; set; }
    }
}
