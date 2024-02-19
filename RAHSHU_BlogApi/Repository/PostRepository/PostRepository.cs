using Microsoft.EntityFrameworkCore;
using RAHSHU_BlogApi.Data;
using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Repository.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Post> AddAsync(Post newPost)
        {
            await _context.Posts.AddAsync(newPost);
            return newPost;
        }

        public async Task DeleteAsync(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<IEnumerable<Post>> GetPaging(int skip, int take = 10)
        {
            return await _context.Posts
                .OrderByDescending(p => p.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();  
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Post> UpdateAsync(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
