namespace VoiceWall.Web.ViewModels.Upload
{
    using System.Web;

    using VoiceWall.Web.Infrastructure.Filters;

    public class NewPictureContentInputModel
    {
        [ValidatePictureFile]
        public HttpPostedFileBase File { get; set; }
    }
}