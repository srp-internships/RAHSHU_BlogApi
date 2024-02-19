using AutoMapper;
using RAHSHU_BlogApi.Dtos.UsersDto;
using RAHSHU_BlogApi.Repository.UserRepository;

namespace RAHSHU_BlogApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<GetUserDto>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return _mapper.Map<List<GetUserDto>>(users);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetUserDto>> Search(string username)
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                if (!string.IsNullOrEmpty(username))
                {
                    users = users.Where(u =>
                    u.FirstName.Contains(username) ||
                    u.LastName.Contains(username) ||
                    u.Username.Contains(username)
                    );
                }

                var filteredUserDto = _mapper.Map<IEnumerable<GetUserDto>>(users);

                return filteredUserDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
