

//using New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace New.RequestModel
{
    public class FoldersRequest
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }


    }
}
