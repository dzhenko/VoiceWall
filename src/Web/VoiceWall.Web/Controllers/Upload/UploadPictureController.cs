namespace VoiceWall.Web.Controllers.Upload
{
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;

    public class UploadPictureController : BaseUploadController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}