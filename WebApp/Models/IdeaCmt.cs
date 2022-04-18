using Project1640.Dto.Comment;
using Project1640.Dto.Ideas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class IdeaCmt
    {
        public IdeaDto Idea { get; set; }
        public List<CommentDto> Cmt { get; set; }

    }
}
