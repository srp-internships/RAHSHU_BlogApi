using RAHSHU_BlogApi.Services.JsonPlaceholderService;
using RAHSHU_BlogApi.Repository.UserRepository;

namespace RAHSHU_BlogApi.Services.SeedService
{
    public class SeedService : ISeedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJsonPlaceholderService _jsonPlaceholderService;

        public SeedService(IUserRepository userRepository, IJsonPlaceholderService jsonPlaceholderService)
        {
            _userRepository = userRepository;
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        public async Task Seed()
        {
            var users = await _jsonPlaceholderService.FetchUser();
            var posts = await _jsonPlaceholderService.FetchPost();

            foreach (var user in users)
            {
                var userPosts = posts.Where(p => p.UserId == user.Id);
                user.Posts = userPosts.ToList();
                user.Id = 0;
            }



            

            await _userRepository.Create(users);
            await _userRepository.SaveChangesAsync();
        }
    }
}