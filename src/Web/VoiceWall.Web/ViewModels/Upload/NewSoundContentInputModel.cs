namespace VoiceWall.Web.ViewModels.Upload
{
    using System.Web;

    using VoiceWall.Web.Infrastructure.Filters;

    public class NewSoundContentInputModel
    {
        [ValidateSoundFile]
        public HttpPostedFileBase File { get; set; }
    }
}