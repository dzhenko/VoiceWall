namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;

    public class UploadSoundController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadSoundController(ISoundsCloudStorage soundsCloudStorageProvider)
        {
            this.storage = soundsCloudStorageProvider;
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