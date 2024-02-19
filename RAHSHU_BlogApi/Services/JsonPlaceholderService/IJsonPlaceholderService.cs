using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Services.JsonPlaceholderService
{
    public interface IJsonPlaceholderService
    {
        Task<List<User>> FetchUser();
        Task<List<Post>> FetchPost();
    }
}
