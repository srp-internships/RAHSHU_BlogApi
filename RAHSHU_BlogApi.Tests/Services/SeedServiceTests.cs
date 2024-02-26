using Moq;
using RAHSHU_BlogApi.Data;
using RAHSHU_BlogApi.Models;
using RAHSHU_BlogApi.Services;
using RAHSHU_BlogApi.Services.JsonPlaceholderService;
using RAHSHU_BlogApi.Services.SeedService;

namespace RAHSHU_BlogApi.Tests.Services
{
    [TestFixture]
    public class SeedServiceTests
    {
        private SeedService _seedService;
        private Mock<DataContext> _mockContext;
        private Mock<IHttpClientService> _mockHttpClientService;
        private Mock<IJsonPlaceholderService> _mockJsonPlaceholderService;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<DataContext>();
            _mockHttpClientService = new Mock<IHttpClientService>();
            _mockJsonPlaceholderService = new Mock<IJsonPlaceholderService>();
            _seedService = new SeedService(_mockContext.Object, _mockHttpClientService.Object, _mockJsonPlaceholderService.Object);
        }

        [Test]
        public async Task Seed_ShouldSeedUsersAndPosts()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1 }, new User { Id = 2 } };
            var posts = new List<Post> { new Post { UserId = 1 }, new Post { UserId = 2 }, new Post { UserId = 1 } };

            _mockJsonPlaceholderService.Setup(s => s.FetchUser()).ReturnsAsync(users);
            _mockJsonPlaceholderService.Setup(s => s.FetchPost()).ReturnsAsync(posts);

            // Act
            await _seedService.Seed();

            // Assert
            _mockContext.Verify(c => c.Users.AddRange(users), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        
    }
}