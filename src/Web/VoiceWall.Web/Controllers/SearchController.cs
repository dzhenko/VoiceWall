namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    public class SearchController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchAll(string search)
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Results()
        {
            return View();
        }
    }
}