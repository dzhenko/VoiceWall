namespace VoiceWall.Web.Controllers
{
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

        public ActionResult Voice()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVoice(HttpPostedFileBase waveBlobFile)
        {
            return Json(new { success = true });
        }
    }
}