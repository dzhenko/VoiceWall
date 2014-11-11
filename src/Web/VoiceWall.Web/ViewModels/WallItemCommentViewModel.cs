namespace VoiceWall.Web.ViewModels
{
    using System;
    using System.Linq;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class WallItemCommentViewModel : IMapFrom<Comment>, IMapCustom
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public int Flags { get; set; }

        // has the user flagged this comment
        public bool IsFlagged { get; set; }

        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, WallItemCommentViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            configuration.CreateMap<Comment, WallItemCommentViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName));

            configuration.CreateMap<Comment, WallItemCommentViewModel>()
                .ForMember(m => m.UserImage, opt => opt.MapFrom(c => c.User.UserImage));

            configuration.CreateMap<Comment, WallItemCommentViewModel>()
                .ForMember(m => m.Flags, opt => opt.MapFrom(c => c.CommentViews.Count(v => v.Flagged == true)));

            configuration.CreateMap<Comment, WallItemCommentViewModel>()
                .ForMember(m => m.IsFlagged, opt => opt.MapFrom(c => false));
        }
    }
}
