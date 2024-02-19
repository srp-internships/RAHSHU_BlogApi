using RAHSHU_BlogApi.Dtos.PostsDto;
using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi.Services.PostService
{
    public interface IPostService
    {
        Task<List<GetPostDto>> GetAllPost();
        Task<IEnumerable<Post>> GetPaging(int skip, int take = 10);
        Task<List<GetPostDto>> AddPost(AddPostDto newPost);
        Task<GetPostDto> UpdateUser(int postId, UpdatePostDto updatePost);
        Task<Post> DeletePost(int postId);
        Task<IEnumerable<GetPostDto>> GetUserPosts(int userId);
    }
}
