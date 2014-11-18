namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}