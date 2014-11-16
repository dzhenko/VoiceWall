namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class ContentViewAdministrationViewModel : AdministrationViewModel, IMapFrom<ContentView>, IMapCustom
    {
        public bool IsHidden { get; set; }

        public bool? Liked { get; set; }

        public bool Flagged { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ContentView, ContentAdministrationViewModel>()
                .ReverseMap();
        }
    }
}