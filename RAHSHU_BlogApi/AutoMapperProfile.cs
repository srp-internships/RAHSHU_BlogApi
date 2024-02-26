using AutoMapper;
using RAHSHU_BlogApi.Dtos.PostsDto;
using RAHSHU_BlogApi.Dtos.UsersDto;
using RAHSHU_BlogApi.Models;

namespace RAHSHU_BlogApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();

            CreateMap<Post, GetPostDto>();
            CreateMap<AddPostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
        }
    }
}
