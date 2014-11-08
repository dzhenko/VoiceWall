namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

    public class HomeController : Controller
    {
        public HomeController(IRepository<User> repo, IVideosCloudStorage storage /*di test*/)
        {
            
        }

        //[Authorize]
        //[OutputCache(Duration = 10, VaryByCustom = "User")]
        public ActionResult Index()
        {
            return View(ViewModels.FakeDataSeeder.GetWallItems(15));
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
        public ActionResult UploadVideoAndAudio(HttpPostedFileBase videoAndAudioFile, string wallItemId /*create input model ?*/)
        {
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVideo(HttpPostedFileBase videoFile, string wallItemId /*create input model ?*/)
        {
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        public ActionResult Voice()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVoice(HttpPostedFileBase waveBlobFile, string wallItemId /*create input model ?*/)
        {
            // magic
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        public ActionResult Picture()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadPicture(HttpPostedFileBase imageFile, string wallItemId /*create input model ?*/)
        {
            // magic
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }
    }
}