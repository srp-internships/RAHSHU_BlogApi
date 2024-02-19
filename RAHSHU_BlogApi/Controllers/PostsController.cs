using Microsoft.AspNetCore.Mvc;
using RAHSHU_BlogApi.Dtos.PostsDto;
using RAHSHU_BlogApi.Services.PostService;

namespace RAHSHU_BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetPostDto>>> GetAll()
        {
            return Ok(await _postService.GetAllPost());
        }

        [HttpGet("GetPaging")]
        public async Task<IActionResult> GetPaging(int skip, int take)
        {
            return Ok(await _postService.GetPaging(skip,take));
        }

        [HttpGet("GetPostByUserId")]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> GetUserPosts (int userId)
        {
            return Ok(await _postService.GetUserPosts(userId));
        }

        [HttpPost("AddPost")]
        public async Task<ActionResult<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            return Ok(await _postService.AddPost(newPost));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetPostDto>> UpdatePost(int id, UpdatePostDto updatePost)
        {
            try
            {
                var updatedUser = await _postService.UpdateUser(id, updatePost);
                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                await _postService.DeletePost(postId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the post.");
            }
        }
    }
}
