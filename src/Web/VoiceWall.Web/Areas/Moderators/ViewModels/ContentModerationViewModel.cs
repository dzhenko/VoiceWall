namespace VoiceWall.Web.Areas.Moderators.ViewModels
{
    using System.Linq;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class ContentModerationViewModel : ModerationViewModel, IMapFrom<Content>, IMapCustom
    {
        public string ContentUrl { get; set; }

        public string Owner { get; set; }

        public int Flags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Content, ContentModerationViewModel>()
                .ForMember(m => m.Owner, opt => opt.MapFrom(c => c.User.Email))
                .ForMember(m => m.Flags, opt => opt.MapFrom(c => c.ContentViews.Count(v => v.Flagged)));
        }
    }
}