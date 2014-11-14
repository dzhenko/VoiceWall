namespace VoiceWall.Web.ViewModels.Account
{
    using System.Web;

    using VoiceWall.Web.Infrastructure.Filters;

    public class UpdateProfileViewModel
    {
        [ValidatePictureFile]
        public HttpPostedFileBase File { get; set; }
    }
}
