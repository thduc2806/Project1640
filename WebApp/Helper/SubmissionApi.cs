using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Conntants;

namespace WebApp.Helper
{
	public class SubmissionApi : BaseApiClient, ISubmissionApi
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		public SubmissionApi(IHttpClientFactory httpClientFactory,
				   IHttpContextAccessor httpContextAccessor,
					IConfiguration configuration)
			: base(httpClientFactory, httpContextAccessor, configuration)
		{
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}

        public async Task<bool> CreateSub(SubmissionAddRequest subRequest)
        {
			var sessions = _httpContextAccessor
				.HttpContext
				.Session
				.GetString(SystemContants.AppSettings.Jwt);

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

			var requestContent = new MultipartFormDataContent();

			requestContent.Add(new StringContent(subRequest.Name.ToString()), "name");
			requestContent.Add(new StringContent(subRequest.Description.ToString()), "description");
			requestContent.Add(new StringContent(subRequest.CloseDate.ToString()), "closeDate");
			requestContent.Add(new StringContent(subRequest.FinalClosureDate.ToString()), "finalClosureDate");


			var response = await client.PostAsync($"/api/submission/", requestContent);

			return response.IsSuccessStatusCode;
		}

        public async Task<List<SubmissionDto>> GetAllSub()
		{
			var data = await GetListAsync<SubmissionDto>("api/submission");
			return data;
		}

		public async Task<SubmissionDto> GetSubById(int subId)
		{
			var data = await GetAsync<SubmissionDto>($"api/submission/{subId}");
			return data;
		}
	}
}
