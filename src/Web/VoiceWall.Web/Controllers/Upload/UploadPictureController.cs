namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;

    public class UploadPictureController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadPictureController(IPicturesCloudStorage picturesCloudStorageProvider)
        {
            this.storage = picturesCloudStorageProvider;
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