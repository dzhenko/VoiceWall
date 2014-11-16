namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class CommentAdministrationViewModel : AdministrationViewModel, IMapFrom<Comment>, IMapCustom
    {
        [DataType(DataType.Url)]
        public string ContentUrl { get; set; }

        public bool IsHidden { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentAdministrationViewModel>()
                .ReverseMap();
        }
    }
}