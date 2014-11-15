namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using VoiceWall.Services.Common.Fetchers;

    public class SearchController : BaseController
    {
        private IContentFetcherService contentFetcherService;

        public SearchController(IContentFetcherService contentFetcherService)
        {
            this.contentFetcherService = contentFetcherService;
        }

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