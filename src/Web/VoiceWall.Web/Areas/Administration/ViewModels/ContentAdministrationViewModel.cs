namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class ContentAdministrationViewModel : AdministrationViewModel, IMapFrom<Content>, IMapCustom
    {
        [UIHint("MultiLineText")]
        public string ContentUrl { get; set; }

        public bool IsHidden { get; set; }

        [UIHint("MultiLineText")]
        public string UserEmail { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Content, ContentAdministrationViewModel>()
                .ForMember(m => m.UserEmail, opt => opt.MapFrom(c => c.User.Email));
        }
    }
}