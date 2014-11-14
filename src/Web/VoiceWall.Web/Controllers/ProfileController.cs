namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ProfileController : Controller
    {
        public ActionResult Details(Guid id)
        {
            return this.View();
        }
    }
}