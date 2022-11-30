using AutoMapper;
using Blog.API.DTOs;
using Blog.DAL.Entities;
using Blog.Service.Common;


namespace Blog.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogPostForCreationDto, BlogPost>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.TagList != null && x.TagList.Any() ? string.Join(",", x.TagList) : ""))
                .ForMember(x => x.Slug, opt => opt.MapFrom(x => SlugGenerator.ToUrlSlug(x.Title)))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title.Trim()));
            

            CreateMap<BlogPost, BlogPostDto>().ForMember(x => x.TagList, opt => opt.MapFrom(x => x.Tags.Split(",", StringSplitOptions.None)));

            CreateMap<BlogPostForUpdateDto, BlogPost>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentForCreationDto, Comment>();
        }
    }
}
