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
        public async Task<IActionResult> GetSubById(int subId)
        {
            var sub = await _submissionService.GetSubById(subId);
            if (sub == null)
                return BadRequest("Can not find");
            return Ok(sub);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateSub([FromForm] SubmissionAddRequest request)
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
