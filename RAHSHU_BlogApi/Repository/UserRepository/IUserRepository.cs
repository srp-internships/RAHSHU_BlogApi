using Microsoft.AspNetCore.Http.HttpResults;
using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task Create(List<User> users);
        Task SaveChangesAsync();
    }
}
