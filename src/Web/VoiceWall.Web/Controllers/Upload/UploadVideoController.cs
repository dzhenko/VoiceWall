namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;

    public class UploadVideoController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadVideoController(ICloudStorage cloudStorageProvider)
        {
            this.storage = cloudStorageProvider;
        }

        public ActionResult Create(HttpPostedFileBase file)
        {
            this.ValidateVideo(file, ModelState);
            return View();
        }

        public ActionResult Comment()
        {
            return View();
        }

        private void ValidateVideo(HttpPostedFileBase file, ModelStateDictionary ModelState)
        {
            if (file == null)
            {

            }

            if (file.ContentLength > long.Parse(ConfigurationManager.AppSettings["PicturesCloudStorage"]))
            {

            }

            throw new System.NotImplementedException();
        }
    }
}