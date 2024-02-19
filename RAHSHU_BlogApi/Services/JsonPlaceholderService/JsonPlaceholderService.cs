using RAHSHU_BlogApi.Dtos.SeedDto;
using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Services.JsonPlaceholderService
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly IHttpClientService httpClientService;

        public JsonPlaceholderService(IHttpClientService _httpClientService)
        {
            httpClientService = _httpClientService;
        }

        public async Task<List<Post>> FetchPost()
        {
            var postDtos = await httpClientService.GetAsync<List<PostDto>>("https://jsonplaceholder.typicode.com/", "posts");
            var posts = postDtos.Select(x => new Post { Title = x.Title, Body = x.Body, UserId = x.UserId }).ToList();

            return posts;
        }

        public async Task<List<User>> FetchUser()
        {
            var usersDto = await httpClientService.GetAsync<List<UserDto>>("https://jsonplaceholder.typicode.com/", "users");
            
            var users = usersDto.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.Name.Split(" ")[0],
                LastName = x.Name.Split(" ")[1],
                Address = $"{x.Address.City} {x.Address.Street}",
                CompanyName = x.Company.Name,
                Phone = x.Phone,
                Username = x.Username,
                Email = x.Email
            }).ToList();

            return users;
        }
    }
}
