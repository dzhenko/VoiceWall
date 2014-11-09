namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;

    public class UploadPictureController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadPictureController(IPicturesCloudStorage picturesCloudStorageProvider)
        {
            this.storage = picturesCloudStorageProvider;
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewPictureContentInputModel model)
        {
            return View();
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(NewPictureCommentInputModel model)
        {
            return View();
        }
    }
}