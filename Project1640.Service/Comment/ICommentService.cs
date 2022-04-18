using Project1640.Dto.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Comment
{
    public interface ICommentService
    {
        public Task<int> CreateComment(CreateCommentRequest request);
        public Task<List<CommentDto>> GetCommentByIdea(int ideaId);
    }
}
