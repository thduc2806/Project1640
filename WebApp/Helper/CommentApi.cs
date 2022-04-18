using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Project1640.Dto.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Conntants;

namespace WebApp.Helper
{
    public class CommentApi : BaseApiClient,ICommentApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CommentApi(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateCmt(CreateCommentRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session;
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(request.Content.ToString()), "content");
            requestContent.Add(new StringContent(request.IdeaId.ToString()), "ideaId");
            requestContent.Add(new StringContent(request.UserId.ToString()), "userId");
            var response = await client.PostAsync($"/api/comment/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CommentDto>> GetAllByIdea(int ideaId)
        {
            var data = await GetListAsync<CommentDto>($"/api/comment/idea/{ideaId}");

            return data;
        }
    }
}
