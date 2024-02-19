using Microsoft.EntityFrameworkCore;
using RAHSHU_BlogApi.Data;
using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(List<User> users)
        {
            try
            {
                await _context.Users.AddRangeAsync(users);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during user creation: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
