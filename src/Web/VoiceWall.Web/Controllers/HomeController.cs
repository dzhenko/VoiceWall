namespace VoiceWall.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using VoiceWall.Data;
    using VoiceWall.Web.ViewModels;
    using VoiceWall.Services.Common.Fetchers;
    using VoiceWall.Web.Infrastructure.Filters;

    [Authorize]
    public class HomeController : BaseController
    {
        private IContentFetcherService contentFetcherService;

        public HomeController(IContentFetcherService contentFetcherService)
        {
            this.contentFetcherService = contentFetcherService;
        }

        public ActionResult Index()
        {
            return this.ConditionalActionResult(() => this.contentFetcherService.GetLast().Project().To<WallItemViewModel>(),
                                                      (wallItems) => this.View(wallItems));
        }

        [AjaxGet]
        public ActionResult More(int id = 5)
        {
            return this.ConditionalActionResult(() => this.contentFetcherService.GetNext(id).Project().To<WallItemViewModel>(),
                                                      (wallItems) => this.PartialView("_WallItemMultiplePartials", wallItems));
        }
    }
}