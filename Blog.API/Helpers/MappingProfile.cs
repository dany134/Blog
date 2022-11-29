using AutoMapper;
using Blog.API.DTOs;
using Blog.DAL.Entities;
using Microsoft.Extensions.Options;

namespace Blog.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogPostForCreationDto, BlogPost>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.TagList != null && x.TagList.Any() ? string.Join(",", x.TagList) : ""))
                .ForMember(x => x.Slug, opt => opt.MapFrom(x => x.Title.Contains(" ") ? x.Title.Trim().Replace(" ", "-").ToLower() : x.Title.ToLower().Trim()));

            CreateMap<BlogPost, BlogPostDto>().ForMember(x => x.TagList, opt => opt.MapFrom(x => x.Tags.Split(",", StringSplitOptions.None)));

            CreateMap<BlogPostForUpdateDto, BlogPost>().ForMember(x => x.Tags, opt => opt.MapFrom(x => x.TagList != null && x.TagList.Any() ? string.Join(",", x.TagList) : ""));
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentForCreationDto, Comment>();
        }
    }
}
