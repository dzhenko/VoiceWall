namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    public class StaticPagesController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}