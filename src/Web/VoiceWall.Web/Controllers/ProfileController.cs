namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(Guid userId)
        {
            return null;
        }
    }
}