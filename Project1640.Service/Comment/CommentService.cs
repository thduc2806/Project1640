using Microsoft.EntityFrameworkCore;
using Project1640.Data.EF;
using Project1640.Data.Entities;
using Project1640.Dto.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Comment
{
    public class CommentService : ICommentService
    {
        private readonly Project1640DbContext _context;
        public CommentService(Project1640DbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateComment(CreateCommentRequest request)
        {
            var cmt = new Cmt()
            {
                content = request.Content,
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                IdeaId = request.IdeaId,
                UserId = request.UserId
            };
            _context.Cmts.Add(cmt);
            await _context.SaveChangesAsync();
            return cmt.CmtId;
        }

        public async Task<List<CommentDto>> GetCommentByIdea(int ideaId)
        {
            var query = from c in _context.Cmts
                        join i in _context.Ideas on c.IdeaId equals i.IdeaId
                        join u in _context.Users on c.UserId equals u.Id
                        where c.IdeaId == ideaId
                        select new { c, i, u };
            var data = await query.Select(z => new CommentDto()
            {
                CmtId = z.c.CmtId,
                Content = z.c.content,
                CreateDate = z.c.CreateDate,
                LastModifiedDate = z.c.LastModifiedDate,
                IdeaId = z.i.IdeaId,
                UserId = z.u.Id
            }).ToListAsync();
            return data;


        }
    }
}
