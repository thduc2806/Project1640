using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project1640.Data.EF;
using Project1640.Data.Entities;
using Project1640.Dto.Exceptions;
using Project1640.Dto.Files;
using Project1640.Dto.Ideas;
using Project1640.Service.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Ideas
{
    public class IdeaService : IIdeaService
    {
        private readonly Project1640DbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public IdeaService(Project1640DbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

		public async Task<int> AddImage(int productId, FilesAddRequest request)
		{
            var file = new Data.Entities.File()
            {
                CreatedDate = DateTime.Now,
            };

            if (request.Files != null)
			{
                file.FilePath = await this.SaveFile(request.Files);
			}
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
            return file.FileId;
		}
		public async Task<FilesDto> GetFileById(int fileId)
		{
            var files = await _context.Files.FindAsync(fileId);
            if (files == null)
                throw new Project1640Exception("Can not find Files");
            var fileVm = new FilesDto()
            {
                FileId = files.FileId,
                IdeaId = files.IdeaId,
                FilePath = files.FilePath,
                CreatedDate = DateTime.Now,
            };
            return fileVm;
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
                    LastModifiedDate = DateTime.Now,
                    CategoryId = request.CategoryId,
                    UserId = request.UserId,
                    SubmissionId = request.SubmissionId,
                };
                idea.Files = new List<Data.Entities.File>()
                {
                    new Data.Entities.File
                    {
                        CreatedDate = DateTime.Now,
                        FilePath = await this.SaveFile(request.ThumbnailFile),
                    }
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
            var file = await _context.Files.Where(x => idea.IdeaId == x.IdeaId).FirstOrDefaultAsync();
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
                FilePath = file.FilePath
                
            };
            return ideaView;
        }
        public async Task<List<IdeaDto>> GetAllIdeaBySubId(int subId)
        {
            var query = from i in _context.Ideas
                        join s in _context.Submissions on i.SubmissionId equals s.SubmissionId
                        join c in _context.Categories on i.CategoryId equals c.CategoryId
                        join u in _context.Users on i.UserId equals u.Id
                        join f in _context.Files on i.IdeaId equals f.IdeaId
                        where i.SubmissionId == subId
                        select new { i, s, c, u, f };
         

            var data = await query.Select(z => new IdeaDto()
            {
                IdeaId = z.i.IdeaId,
                Content = z.i.Content,
                Description = z.i.Description,
                CreatedDate = z.i.CreatedDate,
                LastModifiedDate = z.i.LastModifiedDate,
                Title = z.i.Title,
                FilePath = z.f.FilePath,
                Category = z.c.Name,
                Submission = z.s.Name,
                UserName = z.u.UserName,
            }).ToListAsync();
            return data;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

    }
}
