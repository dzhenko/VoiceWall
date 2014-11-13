namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    public class SearchController : BaseController
    {
        public ActionResult All(string search)
        {
            ViewBag.Search = search;
            return View();
        }
    }
}