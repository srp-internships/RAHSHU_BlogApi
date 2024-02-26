using AutoMapper;
using Moq;
using RAHSHU_BlogApi.Dtos.UsersDto;
using RAHSHU_BlogApi.Models;
using RAHSHU_BlogApi.Repository.UserRepository;
using RAHSHU_BlogApi.Services.UserService;

namespace RAHSHU_BlogApi_UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockMapper.Object, _mockUserRepository.Object);
        }

        [Test]
        public async Task GetAllUsers_ShouldReturnListOfUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "John", LastName = "Doe", Username = "john.doe" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "jane.smith" }
            };
            var userDtos = new List<GetUserDto>
            {
                new GetUserDto { Id = 1, FirstName = "John", LastName = "Doe", Username = "john.doe" },
                new GetUserDto { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "jane.smith" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
            _mockMapper.Setup(mapper => mapper.Map<List<GetUserDto>>(users)).Returns(userDtos);

            var result = await _userService.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(users.Count, result.Count);
            CollectionAssert.AreEqual(userDtos, result);
        }

        [Test]
        public async Task Search_ShouldReturnFilteredUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "John", LastName = "Doe", Username = "john.doe" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "jane.smith" }
            };
            var userDtos = new List<GetUserDto>
            {
                new GetUserDto { Id = 1, FirstName = "John", LastName = "Doe", Username = "john.doe" }
            };
            var searchUsername = "John";

            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<GetUserDto>>(It.IsAny<IEnumerable<User>>())).Returns(userDtos);

            var result = await _userService.Search(searchUsername);

            Assert.IsNotNull(result);
            Assert.AreEqual(userDtos.Count, result.Count());
            CollectionAssert.AreEqual(userDtos, result);
        }
    }
}
