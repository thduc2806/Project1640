using Project1640.Dto.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{
    public interface ICommentApi
    {
        Task<bool> CreateCmt(CreateCommentRequest request);
        Task<List<CommentDto>> GetAllByIdea(int ideaId);
    }
}
