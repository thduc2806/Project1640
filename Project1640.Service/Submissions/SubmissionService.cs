using Microsoft.EntityFrameworkCore;
using Project1640.Data.EF;
using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Submissions
{
    public class SubmissionService : ISubmissionService
    {
        private readonly Project1640DbContext _context;
        public SubmissionService(Project1640DbContext context)
        {
            _context = context;
        }
        public async Task<List<SubmissionDto>> GetAllSub()
        {
            var query = from s in _context.Submissions select new { s };
            var sub = await query.Select(x => new SubmissionDto()
            {
                SubId = x.s.SubmissionId,
                Name = x.s.Name,
                Description = x.s.Description,
                CloseDate = x.s.ClosureDate,
                FinalClosureDate = x.s.FinalClosureDate,
            }).ToListAsync();
            return sub;
        }

        public async Task<SubmissionDto> GetSubById(int subId)
        {
            var sub = await _context.Submissions.FindAsync(subId);
            var s =  new SubmissionDto()
            {
                SubId = sub.SubmissionId,
                Name = sub.Name,
                Description = sub.Description,
                CloseDate = sub.ClosureDate,
                FinalClosureDate = sub.FinalClosureDate,
            };

            return s;
        }
    }
}
