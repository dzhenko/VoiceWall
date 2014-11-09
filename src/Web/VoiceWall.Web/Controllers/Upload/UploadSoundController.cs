namespace VoiceWall.Web.Controllers.Upload
{
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;

    public class UploadSoundController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadSoundController(ISoundsCloudStorage soundsCloudStorageProvider)
        {
            this.storage = soundsCloudStorageProvider;
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewSoundContentInputModel model)
        {
            return View();
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(NewSoundCommentInputModel model)
        {
            return View();
        }
    }
}