namespace VoiceWall.Web.Controllers.Partials
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.Web.ViewModels;
    using VoiceWall.Data.Models;

    using VoiceWall.Services.Common.Fetchers;

    public class WallItemPartialController : BaseController
    {
        private const string PartialViewName = "_WallItemPartial";

        private readonly IContentFetcherService contentFetcherService;

        public WallItemPartialController(IContentFetcherService contentFetcherService)
        {
            this.contentFetcherService = contentFetcherService;
        }

        [ChildActionOnly]
        public ActionResult GetFromId(Guid id)
        {
            return this.GetFromQueryable(this.contentFetcherService.GetById(id));
        }

        [ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Content> queryable)
        {
            return this.GetFromViewModel(queryable.Project().To<WallItemViewModel>().FirstOrDefault());
        }

        [ChildActionOnly]
        public ActionResult GetFromViewModel(WallItemViewModel viewModel)
        {
            return this.PartialView(PartialViewName, viewModel);
        }
    }
}