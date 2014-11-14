namespace VoiceWall.Web.ViewModels.Profile
{
    using System.Collections.Generic;

    using VoiceWall.Web.ViewModels.Account;

    public class ProfileDetailsViewModel
    {
        public SingleProfileViewModel Profile { get; set; }

        public IEnumerable<WallItemViewModel> WallItems { get; set; }
    }
}