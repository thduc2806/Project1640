using Project1640.Dto.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Ideas
{
    public class IdeaDto
    {
        public int IdeaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string UserName { get; set; }
        public string Category { get; set; }
        public string Submission { get; set; }
		public string FilePath { get; set; }
        public string TextCmt { get; set; }
        public List<CommentDto> CommentDto { get; set; }
        public List<string> Cmt { get; set; } = new List<string>();


    }
}
