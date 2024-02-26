using AutoMapper;
using RAHSHU_BlogApi.Dtos.PostsDto;
using RAHSHU_BlogApi.Models;
using RAHSHU_BlogApi.Repository.PostRepository;

namespace RAHSHU_BlogApi.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostService(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public async Task<List<GetPostDto>> AddPost(AddPostDto newPost)
        {
            var post = _mapper.Map<Post>(newPost);
            var addPost = await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();
            var addpostDto = _mapper.Map<GetPostDto>(addPost);
            return new List<GetPostDto> { addpostDto };
        }

        public async Task<Post> DeletePost(int postId)
        {
            await _postRepository.DeleteAsync(postId);
            return null;

        }

        public async Task<List<GetPostDto>> GetAllPost()
        {
            var users = await _postRepository.GetAllAsync();
            return _mapper.Map<List<GetPostDto>>(users);
        }

        public async Task<IEnumerable<Post>> GetPaging(int skip, int take = 10)
        {
            if (skip < 0)
            {
                throw new ArgumentException("Skip parameter cannot be negative.");
            }

            if (take <= 0)
            {
                throw new ArgumentException("Take parameter must be greater than zero.");
            }

            return await _postRepository.GetPaging(skip, take);
        }

        public async Task<IEnumerable<GetPostDto>> GetUserPosts(int userId)
        {
            var post = await _postRepository.GetPostsByUserId(userId);
            return _mapper.Map<IEnumerable<GetPostDto>>(post);
        }

        public async Task<GetPostDto> UpdateUser(int postId, UpdatePostDto updatePost)
        {
            var existingUser = await _postRepository.GetByIdAsync(postId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }

            _mapper.Map(updatePost, existingUser);

            var updatedUser = await _postRepository.UpdateAsync(existingUser);

            return _mapper.Map<GetPostDto>(updatedUser);
        }      
    }
}
