namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class CommentAdministrationViewModel : AdministrationViewModel, IMapFrom<Comment>, IMapCustom
    {
        [DataType(DataType.Url)]
        public string ContentUrl { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int Flags { get; set; }

        public Guid ContentId { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentAdministrationViewModel>()
                .ForMember(m => m.Owner, opt => opt.MapFrom(c => c.User.Email))
                .ForMember(m => m.Flags, opt => opt.MapFrom(c => c.CommentViews.Count(v => v.Flagged)))
                .ReverseMap();
        }
    }
}