using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.ServicesModels.Cometchat;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GenericSocialMedia.Persistence.Services
{
    public class CometChatService : ICometChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CometChatService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task CreateUser(string username, string uid, string? avatar = null, string? link = null)
        {
            var url = _configuration["CometChat:ApiEndpoint"] + "/users";
            var body = new CometchatCreateUserRequest
            {
                Uid = uid,
                Name = username,
                Avatar = avatar,
                Link = link
            };
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    {"apiKey", _configuration["CometChat:RestAPIKey"] }
                },

            };
            // "{"Uid":"test-api1","Name":"test-api1","Avatar":null,"Link":null,"Role":null,"Tags":null,"WithAuthToken":null}"
            httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(body),
                                    Encoding.UTF8,
                                    "application/json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        public async Task<CometchatListUsers?> ListUsers(string searchKey)
        {
            var url = _configuration["CometChat:ApiEndpoint"] + "/users?perPage=100&page=1";
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    {"apiKey", _configuration["CometChat:RestAPIKey"] }
                }
            };
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<CometchatListUsers>(responseContent, options);
            return responseObject;
        }
    }
}
