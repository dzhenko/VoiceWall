namespace VoiceWall.Web.ViewModels.Profile
{
    using System.Collections.Generic;
    using System.Linq;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;
    using VoiceWall.Web.ViewModels.Account;

    public class ProfileDetailsViewModel : IMapFrom<User>, IMapCustom
    {
        public SingleProfileViewModel Profile { get; set; }

        public IEnumerable<WallItemViewModel> WallItems { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<User, ProfileDetailsViewModel>()
                .ForMember(m => m.Profile, opt => opt.MapFrom(u => u));

            configuration.CreateMap<User, ProfileDetailsViewModel>()
                .ForMember(m => m.WallItems, opt => opt.MapFrom(u => u.Contents.OrderByDescending(c => c.CreatedOn).Take(5)));
        }
    }
}