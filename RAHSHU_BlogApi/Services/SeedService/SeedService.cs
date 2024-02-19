using RAHSHU_BlogApi.Data;
using RAHSHU_BlogApi.Services.JsonPlaceholderService;

namespace RAHSHU_BlogApi.Services.SeedService
{
    public class SeedService : ISeedService
    {
        private readonly DataContext _context;
        private readonly IHttpClientService _httpClientService;
        private readonly IJsonPlaceholderService jsonPlaceholderService;

        public SeedService(DataContext context, IHttpClientService httpClientService, IJsonPlaceholderService jsonPlaceholderService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
            this.jsonPlaceholderService = jsonPlaceholderService;
        }

        public async Task Seed()
        {
            try
            {
                var users = await jsonPlaceholderService.FetchUser();
                var posts = await jsonPlaceholderService.FetchPost();

                foreach(var user in  users)
                {
                    var userPosts = posts.Where(p => p.UserId == user.Id);
                    user.Posts = userPosts.ToList();
                    user.Id = 0;
                }

                _context.Users.AddRange(users);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
