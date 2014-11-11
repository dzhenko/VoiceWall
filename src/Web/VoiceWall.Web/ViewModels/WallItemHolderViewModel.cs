namespace VoiceWall.Web.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class WallItemHolderViewModel : IMapFrom<Content>, IMapCustom
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Flags { get; set; }

        public bool? IsLiked { get; set; }

        public bool IsFlagged { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            // IsLiked and IsFlagged are left to the ctrl - we dont have access to current user here
            configuration.CreateMap<Content, WallItemHolderViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            configuration.CreateMap<Content, WallItemHolderViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName));

            configuration.CreateMap<Content, WallItemHolderViewModel>()
                .ForMember(m => m.UserImage, opt => opt.MapFrom(c => c.User.UserImage));

            configuration.CreateMap<Content, WallItemHolderViewModel>()
                .ForMember(m => m.Likes, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Liked == true)));

            configuration.CreateMap<Content, WallItemHolderViewModel>()
                .ForMember(m => m.Hates, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Liked == false)));

            configuration.CreateMap<Content, WallItemHolderViewModel>()
                .ForMember(m => m.Flags, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Flagged == true)));
        }
    }
}