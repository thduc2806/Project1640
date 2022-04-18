using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Comment
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }
        public int IdeaId { get; set; }
        public int UserId { get; set; }
    }
}
