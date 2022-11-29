using AutoMapper;
using Blog.API.DTOs;
using Blog.DAL.Entities;

namespace Blog.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogPostForCreationDto, BlogPost>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(x => string.Join(",", x.TagList)))
                .ForMember(x => x.Slug, opt => opt.MapFrom(x => x.Title.Contains(" ") ? x.Title.Trim().Replace(" ", "-").ToLower(): x.Title.ToLower().Trim()));
        }
    }
}
