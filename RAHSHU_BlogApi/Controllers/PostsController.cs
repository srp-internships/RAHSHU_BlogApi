﻿using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var posts = await _postService.GetAllPost();

                if (posts.Any())
                {
                    return Ok(posts);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return NotFound("No Post Found");
            }
        }

        [HttpGet("GetPaging")]
        public async Task<IActionResult> GetPaging(int skip, int take)
        {
            try
            {
                var posts = await _postService.GetPaging(skip, take);

                if (posts.Any())
                {
                    return Ok(posts);
                }
                else
                {
                    return NotFound("No posts found for the requested page.");
                }
            }
            catch (Exception ex)
            {

                return NotFound("No posts found for the requested page.");
            }
        }

        [HttpGet("GetPostByUserId")]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> GetUserPosts (int userId)
        {
            try
            {
                var userPosts = await _postService.GetUserPosts(userId);

                if (userPosts.Any())
                {
                    return Ok(userPosts);
                }
                else
                {
                    return NotFound($"No posts found for user with ID {userId}.");
                }
            }
            catch (Exception ex)
            {

                return NotFound($"User with ID {userId} was not found.");
            }
        }

        [HttpPost("AddPost")]
        public async Task<ActionResult<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            try
            {
                var addedPost = await _postService.AddPost(newPost);
                return Ok(addedPost);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while adding a new post. Please try again later.");
            }
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
