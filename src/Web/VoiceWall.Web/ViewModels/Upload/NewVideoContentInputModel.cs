namespace VoiceWall.Web.ViewModels.Upload
{
    using System.Web;

    using VoiceWall.Web.Infrastructure.Filters;

    public class NewVideoContentInputModel
    {
        [ValidateVideoFile]
        public HttpPostedFileBase File { get; set; }
    }
}