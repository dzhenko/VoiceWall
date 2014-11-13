namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.Web.ViewModels;
    using VoiceWall.Data.Models;

    using VoiceWall.Services.Common.Fetchers;

    public class WallItemPartialController : BasePartialController
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
            return this.PartialActionResult(id.ToString(), 
                () => this.GetFromQueryable(this.contentFetcherService.GetById(id)));
        }

        [ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Content> queryable)
        {
            return this.PartialActionResult(queryable.Select(c => c.Id).FirstOrDefault().ToString(), 
                () => this.GetFromViewModel(queryable.Project().To<WallItemViewModel>().FirstOrDefault()));
        }

        [ChildActionOnly]
        public ActionResult GetFromViewModel(WallItemViewModel viewModel)
        {
            return this.PartialActionResult(viewModel.WallItemHolderViewModel.Id.ToString(), 
                () => this.PartialView(PartialViewName, viewModel));
        }
    }
}