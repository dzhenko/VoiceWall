namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    public class StaticPagesController : Controller
    {
        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            return this.View();
        }
    }
}