using Microsoft.Extensions.Options;
using System.Net;
using TDDTest.API.Config;
using TDDTest.API.Models;

namespace TDDTest.API.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;


        public UserService(HttpClient httpClient,IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;

        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient
                .GetAsync(_apiConfig.Endpoint);
            if (usersResponse.StatusCode == HttpStatusCode.NotFound) 
            {
                return new List<User>();
            }

            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();

            return allUsers.ToList();
        }
    }
}
