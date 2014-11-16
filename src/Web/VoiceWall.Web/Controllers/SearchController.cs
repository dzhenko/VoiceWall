namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.Services.Common.Fetchers;
    using VoiceWall.Web.ViewModels.Account;
    using VoiceWall.Web.ViewModels;
    using VoiceWall.Web.ViewModels.Search;

    [Authorize]
    public class SearchController : BaseController
    {
        private ISearchResultsFetcherService searchResultsFetcherService;

        public SearchController(ISearchResultsFetcherService searchResultsFetcherService)
        {
            this.searchResultsFetcherService = searchResultsFetcherService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchUsers(SearchViewModel search)
        {
            return this.RedirectToAction<SearchController>((c) => c.UserResults(null), new { search = search.SearchText });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchAll(SearchViewModel search)
        {
            return this.RedirectToAction<SearchController>((c) => c.AllResults(null), new { search = search.SearchText });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchVideos(SearchViewModel search)
        {
            return this.RedirectToAction<SearchController>((c) => c.VideosResults(null), new { search = search.SearchText });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchVoices(SearchViewModel search)
        {
            return this.RedirectToAction<SearchController>((c) => c.VoicesResults(null), new { search = search.SearchText });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPictures(SearchViewModel search)
        {
            return this.RedirectToAction<SearchController>((c) => c.PicturesResults(null), new { search = search.SearchText });
        }

        public ActionResult UserResults(string search)
        {
            return this.ConditionalActionResult<IQueryable<SingleProfileViewModel>>
                (() => this.searchResultsFetcherService.SearchUsers(search).Project().To<SingleProfileViewModel>(),
                (users) => this.View(users));
        }

        public ActionResult AllResults(string search)
        {
            return this.ConditionalActionResult<IQueryable<WallItemViewModel>>
                (() => this.searchResultsFetcherService.SearchAll(search).Project().To<WallItemViewModel>(),
                (all) => this.View(all));
        }

        public ActionResult VideosResults(string search)
        {
            return this.ConditionalActionResult<IQueryable<WallItemViewModel>>
                (() => this.searchResultsFetcherService.SearchVideos(search).Project().To<WallItemViewModel>(),
                (videos) => this.View(videos));
        }

        public ActionResult VoicesResults(string search)
        {
            return this.ConditionalActionResult<IQueryable<WallItemViewModel>>
                (() => this.searchResultsFetcherService.SearchVoices(search).Project().To<WallItemViewModel>(),
                (voices) => this.View(voices));
        }

        public ActionResult PicturesResults(string search)
        {
            return this.ConditionalActionResult<IQueryable<WallItemViewModel>>
                (() => this.searchResultsFetcherService.SearchPictures(search).Project().To<WallItemViewModel>(),
                (pictures) => this.View(pictures));
        }
    }
}