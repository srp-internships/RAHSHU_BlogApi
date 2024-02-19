using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Repository.PostRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetPaging(int skip, int take = 10);
        Task<IEnumerable<Post>> GetPostsByUserId(int userId);
        Task<Post> GetByIdAsync(int postId);
        Task<Post> UpdateAsync(Post user);
        Task<Post> AddAsync(Post newPost);
        Task DeleteAsync(int postId);
        Task SaveChangesAsync();
    }
}
