namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;

    public class UploadVideoController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadVideoController(IVideosCloudStorage videosCloudStorageProvider)
        {
            this.storage = videosCloudStorageProvider;
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewVideoContentInputModel model)
        {
            return View();
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(NewVideoCommentInputModel model)
        {
            return View();
        }
    }
}