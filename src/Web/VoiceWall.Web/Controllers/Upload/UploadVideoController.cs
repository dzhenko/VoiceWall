namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;

    public class UploadVideoController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadVideoController(IVideosCloudStorage videosCloudStorageProvider)
        {
            this.storage = videosCloudStorageProvider;
        }

        public ActionResult Create(HttpPostedFileBase file)
        {
            return View();
        }

        public ActionResult Comment()
        {
            return View();
        }
    }
}