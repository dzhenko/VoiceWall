namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class CommentViewAdministrationViewModel : AdministrationViewModel, IMapFrom<CommentView>, IMapCustom
    {
        public bool IsHidden { get; set; }

        public bool Flagged { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CommentView, ContentAdministrationViewModel>()
                .ReverseMap();
        }
    }
}