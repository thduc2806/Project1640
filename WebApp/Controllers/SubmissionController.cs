using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project1640.Dto.Ideas;
using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ISubmissionApi _submissionApi;
        private readonly IIdeaApi _ideaApi;

        public SubmissionController(ISubmissionApi submissionApi, IIdeaApi ideaApi)
        {
            _submissionApi = submissionApi;
            _ideaApi = ideaApi;
        }

        public async Task<IActionResult> Index()
        {
            var data = new HomeVm
            {
                Submission = await _submissionApi.GetAllSub()
			};
            return View(data);
        }

        [HttpGet]
        public IActionResult CreateSub()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateSub([FromForm]SubmissionAddRequest subRequest)
        {
            if (!ModelState.IsValid)
                return View(subRequest);
            var result = await _submissionApi.CreateSub(subRequest);
            if (result)
            {
                TempData["result"] = "AddSuccess";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Add Fail");
            return View(subRequest);
        }

        [HttpGet]
        public async Task<IActionResult> CreateIdea(int subId)
        {
            var sub = await _submissionApi.GetSubById(subId);
            var idea = new CreateIdeaRequest()
            {
                SubmissionId = subId
            };
            return View(idea);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateIdea([FromForm] CreateIdeaRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _ideaApi.CreateIdea(request);
            if (result)
            {
                TempData["result"] = "Add Success";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Add Fail");
            return View(request);
        }

        public async Task<IActionResult> Idea(int subId)
        {
            var idea = await _ideaApi.GetAllIdeaBySubId(subId);
            return View(new SubmissionIdeaVm()
            {
                Idea = idea
            });
        }
    }
}
