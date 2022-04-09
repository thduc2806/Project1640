using Microsoft.EntityFrameworkCore;
using Project1640.Data.EF;
using Project1640.Data.Entities;
using Project1640.Dto.Exceptions;
using Project1640.Dto.Ideas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Ideas
{
    public class IdeaService : IIdeaService
    {
        private readonly Project1640DbContext _context;
        public IdeaService(Project1640DbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateIdea(CreateIdeaRequest request)
        {
            var submission = await _context.Submissions.FirstOrDefaultAsync(s => s.SubmissionId == request.SubmissionId);

            if (submission.ClosureDate >= DateTime.Now)
            {
                var idea = new Idea()
                {
                    Title = request.Title,
                    Description = request.Description,
                    Content = request.Content,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = request.LastModifiedDate,
                    CategoryId = request.CategoryId,
                    UserId = request.UserId,
                    SubmissionId = request.SubmissionId,
                };
                _context.Ideas.Add(idea);
                await _context.SaveChangesAsync();
                return idea.IdeaId;
            }

            throw new Project1640Exception("Time has expired");
        }

        public async Task<IdeaDto> GetIdeaById(int ideaId)
        {
            var idea = await _context.Ideas.FindAsync(ideaId);
            var user = await _context.Users.Where(x => idea.UserId == x.Id).FirstOrDefaultAsync();
            var category = await _context.Categories.Where(x => idea.CategoryId == x.CategoryId).FirstOrDefaultAsync();
            var submission = await _context.Submissions.Where(x => idea.SubmissionId == x.SubmissionId).FirstOrDefaultAsync();
            var ideaView = new IdeaDto()
            {
                IdeaId = idea.IdeaId,
                Title = idea.Title,
                Description = idea.Description,
                Content = idea.Content,
                CreatedDate = idea.CreatedDate,
                LastModifiedDate = idea.LastModifiedDate,
                Category = category.Name,
                UserName = user.UserName,
                Submission = submission.Name,
                
            };
            return ideaView;
        }
    }
}
