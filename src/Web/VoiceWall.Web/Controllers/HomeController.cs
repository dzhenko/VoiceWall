namespace VoiceWall.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data;
    using VoiceWall.Web.ViewModels;
    using VoiceWall.Services.Common.Fetchers;

    public class HomeController : BaseController
    {
        private IContentFetcherService contentFetcherService;

        public HomeController(IContentFetcherService contentFetcherService)
        {
            this.contentFetcherService = contentFetcherService;
        }

        [Authorize]
        [OutputCache(Duration = 10, VaryByCustom = "User")]
        public ActionResult Index()
        {
            return View(this.contentFetcherService.GetLast().Project().To<WallItemViewModel>());
        }
    }
}