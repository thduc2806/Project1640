using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
