using RAHSHU_BlogApi.Dtos.UsersDto;

namespace RAHSHU_BlogApi.Services.UserService
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetAllUsers();
        Task<IEnumerable<GetUserDto>> Search(string username);

    }
}
