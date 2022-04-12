using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project1640.Dto.Common;
using Project1640.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Helper
{
    public class UserApi : IUserApi
    {
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserApi(IHttpClientFactory httpClientFactory,
				   IHttpContextAccessor httpContextAccessor,
					IConfiguration configuration)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<ApiResult<string>> Authenticate(LoginDto request)
		{
			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var response = await client.PostAsync("/api/Authen", httpContent);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ApiSuccessedResult<string>>(await response.Content.ReadAsStringAsync());
			}

			return JsonConvert.DeserializeObject<ApiResultError<string>>(await response.Content.ReadAsStringAsync());
		}
	}
}
