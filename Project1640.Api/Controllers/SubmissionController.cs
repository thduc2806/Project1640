using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1640.Data.EF;
using Project1640.Data.Entities;
using Project1640.Dto.Submissions;
using Project1640.Service.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1640.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly Project1640DbContext _context;
        public SubmissionController(ISubmissionService submissionService, Project1640DbContext context)
        {
            _submissionService = submissionService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSub()
        {
            var sub = await _submissionService.GetAllSub();
            return Ok(sub);
        }

        [HttpGet("{subId}")]
        public Submission Get(int subId)
        {
            var sub = _context.Submissions.Select(s => new Submission
            {
                SubmissionId = s.SubmissionId,
                Name = s.Name,
                Description = s.Description,
                ClosureDate = s.ClosureDate,
                FinalClosureDate = s.FinalClosureDate,
                Ideas = s.Ideas,
            }).Where(a => a.SubmissionId == subId).FirstOrDefault();
            return sub;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubmissionAddRequest request)
        {
            var sub = new Submission()
            {
                Name = request.Name,
                Description = request.Description,
                ClosureDate = request.CloseDate,
                FinalClosureDate = request.FinalClosureDate
            };
            _context.Submissions.Add(sub);
            await _context.SaveChangesAsync();
            if (sub.SubmissionId > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
