namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class CommentViewAdministrationViewModel : AdministrationViewModel, IMapFrom<CommentView>, IMapCustom
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool Flagged { get; set; }

        public string Owner { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CommentView, CommentViewAdministrationViewModel>()
                .ForMember(m => m.Owner, opt => opt.MapFrom(c => c.User.Email))
                .ReverseMap();
        }
    }
}