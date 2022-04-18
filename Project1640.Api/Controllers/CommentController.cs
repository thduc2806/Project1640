using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1640.Dto.Comment;
using Project1640.Service.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1640.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("idea/{ideaId}")]
        public async Task<IActionResult> GetCmtByIdea(int ideaId)
        {
            var cmt = await _commentService.GetCommentByIdea(ideaId);
            if (cmt == null)
                return BadRequest();
            return Ok(cmt);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCmt([FromForm]CreateCommentRequest request)
        {
            var cmt = await _commentService.CreateComment(request);
            if (cmt == 0)
                return BadRequest();
            return Ok(cmt);

        }
    }
}
