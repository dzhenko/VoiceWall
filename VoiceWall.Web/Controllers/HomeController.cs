namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVideo(HttpPostedFileBase videoFile)
        {
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        public ActionResult Voice()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVoice(HttpPostedFileBase waveBlobFile)
        {
            // magic
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }
    }
}