namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(Guid id)
        {
            return View();
        }

        public ActionResult Own()
        {
            return View();
        }
    }
}