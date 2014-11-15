namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class StaticPagesController : BaseController
    {
        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            return this.View();
        }

        public ActionResult FAQ()
        {
            return this.View();
        }

        public ActionResult Home()
        {
            return this.RedirectToActionPermanent<HomeController>((c) => c.Index());
        }
    }
}