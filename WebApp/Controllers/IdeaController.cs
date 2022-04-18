using Microsoft.AspNetCore.Mvc;
using Project1640.Data.EF;
using Project1640.Dto.Comment;
using Project1640.Dto.Ideas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IIdeaApi _ideaApi;
        private readonly ICommentApi _commentApi;
        private readonly Project1640DbContext _context;
        public IdeaController(ICommentApi commentApi, IIdeaApi ideaApi, Project1640DbContext context)
        {
            _commentApi = commentApi;
            _ideaApi = ideaApi;
            _context = context;
        }
        public async Task<IActionResult> Detail(int id)
        {
            var idea = await _ideaApi.GetIdeaById(id);
            ViewBag.idea = idea;
            var cmt = await _commentApi.GetAllByIdea(id);
            ViewBag.cmt = cmt;
            var comment = new CommentDto()
            {
                IdeaId = id
            };

            return View("Detail", comment);
        }

        [HttpPost]
        public ActionResult SendReview(CreateCommentRequest request, int ideaId)
        {
            _commentApi.CreateCmt(request);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Idea", new { id = request.IdeaId });

        }


    }
}
