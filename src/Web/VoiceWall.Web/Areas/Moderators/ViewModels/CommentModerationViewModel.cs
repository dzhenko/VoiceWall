namespace VoiceWall.Web.Areas.Moderators.ViewModels
{
    using System;
    using System.Linq;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class CommentModerationViewModel : ModerationViewModel, IMapFrom<Comment>, IMapCustom
    {
        public string ContentUrl { get; set; }

        public Guid ContentId { get; set; }

        public string Owner { get; set; }

        public int Flags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentModerationViewModel>()
                .ForMember(m => m.Owner, opt => opt.MapFrom(c => c.User.Email))
                .ForMember(m => m.Flags, opt => opt.MapFrom(c => c.CommentViews.Count(v => v.Flagged)));
        }
    }
}