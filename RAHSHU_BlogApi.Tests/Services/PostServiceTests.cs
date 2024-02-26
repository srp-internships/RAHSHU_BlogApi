using AutoMapper;
using Moq;
using RAHSHU_BlogApi.Dtos.PostsDto;
using RAHSHU_BlogApi.Models;
using RAHSHU_BlogApi.Repository.PostRepository;
using RAHSHU_BlogApi.Services.PostService;

namespace RAHSHU_BlogApi_UnitTests.Services
{
    [TestFixture]
    public class PostServiceTests
    {
        private PostService _postService;
        private Mock<IMapper> _mockMapper;
        private Mock<IPostRepository> _mockPostRepository;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockPostRepository = new Mock<IPostRepository>();
            _postService = new PostService(_mockMapper.Object, _mockPostRepository.Object);
        }

        [Test]
        public async Task AddPost_ShouldReturnSinglePostDto()
        {
            // Arrange
            var newPostDto = new AddPostDto
            {

                Title = "Test Post",
                Body = "Test Content"
            };

            var mappedPost = new Post
            {

                Title = "Test Post",
                Body = "Test Content"
            };

            var addedPost = new Post
            {

                Id = 1,
                Title = "Test Post",
                Body = "Test Content"
            };

            var expectedDto = new GetPostDto
            {

                Id = 1,
                Title = "Test Post",
                Body = "Test Content"
            };

            _mockMapper.Setup(m => m.Map<Post>(newPostDto)).Returns(mappedPost);
            _mockPostRepository.Setup(r => r.AddAsync(mappedPost)).ReturnsAsync(addedPost);
            _mockPostRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<GetPostDto>(addedPost)).Returns(expectedDto);

            // Act
            var result = await _postService.AddPost(newPostDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expectedDto, result[0]);
        }

        [Test]
        public async Task DeletePost_ShouldInvokeDeleteAsyncOnce()
        {
            // Arrange
            var postId = 1;

            // Act
            await _postService.DeletePost(postId);

            // Assert
            _mockPostRepository.Verify(x => x.DeleteAsync(postId), Times.Once);
        }

        [Test]
        public void DeletePost_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var postId = 1;
            _mockPostRepository.Setup(x => x.DeleteAsync(postId)).ThrowsAsync(new Exception("Delete failed"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _postService.DeletePost(postId));
        }

        [Test]
        public async Task GetAllPost_ShouldReturnListOfPostDto()
        {
            // Arrange
            var posts = new List<Post>
            {
                new Post { Id = 1, Title = "Post 1", Body = "Content 1" },
                new Post { Id = 2, Title = "Post 2", Body = "Content 2" }
            };
            var expectedDtos = new List<GetPostDto>
            {
                new GetPostDto { Id = 1, Title = "Post 1", Body = "Content 1" },
                new GetPostDto { Id = 2, Title = "Post 2", Body = "Content 2" }
            };
            _mockPostRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(posts);
            _mockMapper.Setup(m => m.Map<List<GetPostDto>>(posts)).Returns(expectedDtos);

            // Act
            var result = await _postService.GetAllPost();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEqual(expectedDtos, result);
        }

        [Test]
        public async Task GetPaging_WithValidSkipAndTake_ShouldReturnListOfPosts()
        {
            // Arrange
            var skip = 0;
            var take = 10;
            var expectedPosts = new List<Post>
            {
                new Post { Id = 1, Title = "Post 1", Body = "Content 1" },
                new Post { Id = 2, Title = "Post 2", Body = "Content 2" }
            };
            _mockPostRepository.Setup(r => r.GetPaging(skip, take)).ReturnsAsync(expectedPosts);

            // Act
            var result = await _postService.GetPaging(skip, take);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Post>>(result);
            CollectionAssert.AreEqual(expectedPosts, result);
        }

        [Test]
        public void GetPaging_WithNegativeSkip_ShouldThrowArgumentException()
        {
            // Arrange
            var skip = -1;
            var take = 10;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _postService.GetPaging(skip, take));
        }

        [Test]
        public void GetPaging_WithZeroTake_ShouldThrowArgumentException()
        {
            // Arrange
            var skip = 0;
            var take = 0;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _postService.GetPaging(skip, take));
        }

        [Test]
        public async Task GetUserPosts_ShouldReturnListOfPostDto()
        {
            // Arrange
            var userId = 1;
            var posts = new List<Post>
            {
                new Post { Id = 1, UserId = 1, Title = "Post 1", Body = "Content 1" },
                new Post { Id = 2, UserId = 1, Title = "Post 2", Body = "Content 2" }
            };
            var expectedDtos = new List<GetPostDto>
            {
                new GetPostDto { Id = 1, Title = "Post 1", Body = "Content 1" },
                new GetPostDto { Id = 2, Title = "Post 2", Body = "Content 2" }
            };
            _mockPostRepository.Setup(r => r.GetPostsByUserId(userId)).ReturnsAsync(posts);
            _mockMapper.Setup(m => m.Map<IEnumerable<GetPostDto>>(posts)).Returns(expectedDtos);

            // Act
            var result = await _postService.GetUserPosts(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<GetPostDto>>(result);
            CollectionAssert.AreEqual(expectedDtos, result);
        }

        [Test]
        public void GetUserPosts_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var userId = 1;
            _mockPostRepository.Setup(r => r.GetPostsByUserId(userId)).ThrowsAsync(new Exception("Repository exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _postService.GetUserPosts(userId));
        }

        [Test]
        public async Task UpdateUser_ShouldReturnUpdatedPostDto_WhenPostExists()
        {
            // Arrange
            var postId = 1;
            var updatePostDto = new UpdatePostDto { Title = "Updated Title", Body = "Updated Content" };
            var existingPost = new Post { Id = postId, Title = "Original Title", Body = "Original Content" };
            var updatedPost = new Post { Id = postId, Title = "Updated Title", Body = "Updated Content" };
            var expectedDto = new GetPostDto { Id = postId, Title = "Updated Title", Body = "Updated Content" };

            _mockPostRepository.Setup(r => r.GetByIdAsync(postId)).ReturnsAsync(existingPost);
            _mockPostRepository.Setup(r => r.UpdateAsync(existingPost)).ReturnsAsync(updatedPost);
            _mockMapper.Setup(m => m.Map(updatePostDto, existingPost)).Returns(existingPost);
            _mockMapper.Setup(m => m.Map<GetPostDto>(updatedPost)).Returns(expectedDto);

            // Act
            var result = await _postService.UpdateUser(postId, updatePostDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto, result);
        }

        [Test]
        public void UpdateUser_ShouldThrowArgumentException_WhenPostNotFound()
        {
            // Arrange
            var postId = 1;
            var updatePostDto = new UpdatePostDto { Title = "Updated Title", Body = "Updated Content" };

            _mockPostRepository.Setup(r => r.GetByIdAsync(postId)).ReturnsAsync((Post)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _postService.UpdateUser(postId, updatePostDto));
        }
    }
}

