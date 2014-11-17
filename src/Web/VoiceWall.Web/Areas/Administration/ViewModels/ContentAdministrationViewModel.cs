namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class ContentAdministrationViewModel : AdministrationViewModel, IMapFrom<Content>, IMapCustom
    {
        [DataType(DataType.Url)]
        public string ContentUrl { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int CommentsCount { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Flags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Content, ContentAdministrationViewModel>()
                .ForMember(m => m.Owner, opt => opt.MapFrom(c => c.User.Email))
                .ForMember(m => m.CommentsCount, opt => opt.MapFrom(c => c.Comments.Count()))
                .ForMember(m => m.Likes, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Liked == true)))
                .ForMember(m => m.Hates, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Liked == false)))
                .ForMember(m => m.Flags, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Flagged)))
                .ReverseMap();
        }
    }
}