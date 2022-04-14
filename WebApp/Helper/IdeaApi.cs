using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Project1640.Dto.Ideas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.Conntants;

namespace WebApp.Helper
{
	public class IdeaApi : BaseApiClient, IIdeaApi
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
        public IdeaApi(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

		public async Task<bool> CreateIdea(CreateIdeaRequest request)
		{
			var sessions = _httpContextAccessor
				.HttpContext
				.Session
				.GetString(SystemContants.AppSettings.Jwt);

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

			var requestContent = new MultipartFormDataContent();

			if (request.ThumbnailFile != null)
			{
				byte[] data;
				using (var br = new BinaryReader(request.ThumbnailFile.OpenReadStream()))
				{
					data = br.ReadBytes((int)request.ThumbnailFile.OpenReadStream().Length);
				}
				ByteArrayContent bytes = new ByteArrayContent(data);
				requestContent.Add(bytes, "thumbnailFile", request.ThumbnailFile.FileName);
			}
				requestContent.Add(new StringContent(request.Content.ToString()), "content");
				requestContent.Add(new StringContent(request.Description.ToString()), "description");
				requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()), "title");
				requestContent.Add(new StringContent(request.SubmissionId.ToString()), "submissionId");
				requestContent.Add(new StringContent(request.CategoryId.ToString()), "categoryId");
				requestContent.Add(new StringContent(request.UserId.ToString()), "userId");




			var response = await client.PostAsync($"/api/idea/submission/" + request.SubmissionId, requestContent);
			return response.IsSuccessStatusCode;
		}

		public async Task<List<IdeaDto>> GetAllIdea()
		{
			var data = await GetListAsync<IdeaDto>($"/api/idea");

			return data;
		}

		public async Task<List<IdeaDto>> GetAllIdeaBySubId(int subId)
		{
			var data = await GetListAsync<IdeaDto>($"/api/idea/submission/{subId}");

			return data;
		}

		public async Task<IdeaDto> GetIdeaById(int ideaId)
		{
			var data = await GetAsync<IdeaDto>($"/api/idea/{ideaId}");

			return data;
		}
	}
}
