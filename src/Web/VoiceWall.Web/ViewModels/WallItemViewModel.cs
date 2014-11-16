namespace VoiceWall.Web.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;

    using VoiceWall.Web.Infrastructure.Mapping;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;

    public class WallItemViewModel : IMapCustom
    {
        public WallItemHolderViewModel WallItemHolderViewModel { get; set; }

        public IEnumerable<WallItemCommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Content, WallItemViewModel>()
                .ForMember(m => m.WallItemHolderViewModel, opt => opt.MapFrom(c => c))
                .ForMember(m => m.Comments, opt => opt.MapFrom(c => c.Comments.OrderByDescending(com => com.CreatedOn)));
        }
    }
}