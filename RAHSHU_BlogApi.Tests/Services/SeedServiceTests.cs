using Moq;
using RAHSHU_BlogApi.Models;
using RAHSHU_BlogApi.Services.SeedService;
using RAHSHU_BlogApi.Services.JsonPlaceholderService;
using RAHSHU_BlogApi.Repository.UserRepository;

namespace RAHSHU_BlogApi.Tests.Services
{
    [TestFixture]
    public class SeedServiceTests
    {
        private SeedService _seedService;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IJsonPlaceholderService> _mockJsonPlaceholderService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJsonPlaceholderService = new Mock<IJsonPlaceholderService>();
            _seedService = new SeedService(_mockUserRepository.Object, _mockJsonPlaceholderService.Object);
        }

        [Test]
        public async Task Seed_ShouldSeedUsersWithPosts()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, FirstName = "User 1" }, new User { Id = 2, FirstName = "User 2" } };
            var posts = new List<Post> { new Post { Id = 1, UserId = 1, Title = "Post 1" }, new Post { Id = 2, UserId = 2, Title = "Post 2" } };

            _mockJsonPlaceholderService.Setup(s => s.FetchUser()).ReturnsAsync(users);
            _mockJsonPlaceholderService.Setup(s => s.FetchPost()).ReturnsAsync(posts);

            // Act
            await _seedService.Seed();

            // Assert
            _mockUserRepository.Verify(r => r.Create(It.IsAny<List<User>>()), Times.Once);
            _mockUserRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}