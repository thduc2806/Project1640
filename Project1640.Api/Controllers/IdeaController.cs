using Microsoft.AspNetCore.Mvc;
using Project1640.Data.EF;
using Project1640.Data.Entities;
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

        [HttpGet]
        public IEnumerable<Idea> GetIdea()
        {
            var idea = _context.Ideas.Select(s => new Idea
            {
                IdeaId = s.IdeaId,
                Title = s.Title,
                Description = s.Description,
                CreatedDate = s.CreatedDate,
                LastModifiedDate = s.LastModifiedDate
            }).ToList();
            return idea;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdeaById(int id)
        {
            var idea = await _ideaService.GetIdeaById(id);
            if (idea == null)
                return BadRequest("Can not find");
            return Ok(idea);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIdea([FromForm] CreateIdeaRequest request)
        {
            var rs = await _ideaService.CreateIdea(request);
            if (rs == 0)
                return BadRequest();
            var idea = await _ideaService.GetIdeaById(rs);
            return CreatedAtAction(nameof(GetIdeaById), new { id = rs }, idea);
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

    }
}
