using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Comment
{
    public class CommentDto
    {
        public int CmtId { get; set; }
        public string Content { get; set; }
        public int IdeaId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
