using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project1640.Data.EF;
using Project1640.Data.Entities;
using Project1640.Dto.Files;
using Project1640.Dto.Ideas;
using Project1640.Service.Ideas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1640.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaController : Controller
    {
        private readonly Project1640DbContext _context;
        private readonly IIdeaService _ideaService;
        public IdeaController(IIdeaService ideaService,Project1640DbContext context)
        {
            _ideaService = ideaService;
            _context = context;
        }

        //[HttpGet]
        //public IEnumerable<Idea> GetIdea()
        //{
        //    var idea = _context.Ideas.Select(s => new Idea
        //    {
        //        IdeaId = s.IdeaId,
        //        Title = s.Title,
        //        Description = s.Description,
        //        CreatedDate = s.CreatedDate,
        //        LastModifiedDate = s.LastModifiedDate,
        //        SubmissionId = s.SubmissionId,
        //        CategoryId = s.CategoryId,
        //        UserId = s.UserId,
        //        Files = s.Files,
        //    }).ToList();
        //    return idea;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllIdea()
		{
            var idea = await _ideaService.GetAllIdea();
            return Ok(idea);
		}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdeaById(int id)
        {
            var idea = await _ideaService.GetIdeaById(id);
            if (idea == null)
                return BadRequest("Can not find");
            return Ok(idea);
        }

        [HttpPost("submission/{subId}")]
        [Consumes("multipart/form-data")]
        //[Authorize]
        public async Task<IActionResult> CreateIdea([FromForm] CreateIdeaRequest request,[FromRoute] int subId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.SubmissionId = subId;
            var rs = await _ideaService.CreateIdea(request);
            if (rs == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdea([FromBody] UpdateIdeaRequest request, int id)
		{
            var idea = _context.Ideas.Find(id);
            idea.Title = request.Title;
            idea.Description = request.Description;
            idea.Content = request.Content;
            idea.LastModifiedDate = request.LastModifiedDate;

            await _context.SaveChangesAsync();
            return Ok("Update Successed");
		}

        [HttpGet("submission/{subId}")]
        public async Task<IActionResult> GetAllIdeaBySubId(int subId)
        {
            var idea = await _ideaService.GetAllIdeaBySubId(subId);
            if (idea == null)
            {
                return BadRequest("Can't not find");
            }
            return Ok(idea);
        }

        [HttpPost("{ideaId}/files")]
        public async Task<IActionResult> CreateImage(int ideaId, [FromForm] FilesAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileId = await _ideaService.AddImage(ideaId, request);
            if (fileId == 0)
                return BadRequest();

            var file = await _ideaService.GetFileById(fileId);

            return CreatedAtAction(nameof(GetFileById), new { FileId = fileId }, file);
        }

        [HttpGet("{ideaId}/files/{fileId}")]
        public async Task<IActionResult> GetFileById(int ideaId, int fileId)
        {
            var file = await _ideaService.GetFileById(fileId);
            if (file == null)
                return BadRequest("Cannot find Idea");
            return Ok(file);
        }

    }
}
