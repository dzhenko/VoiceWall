namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;
    using VoiceWall.Web.Infrastructure.Caching;
    using VoiceWall.Web.ViewModels;

    public class WallItemCommentPartialController : BasePartialController
    {
        private const string WallItemCommentPartialViewName = "_WallItemCommentPartial";

        private readonly ICommentFetcherService commentFetcherService;

        public WallItemCommentPartialController(ICommentFetcherService commentFetcherService, ICacheService cache)
            : base(cache)
        {
            this.commentFetcherService = commentFetcherService;
        }

        [ChildActionOnly]
        public ActionResult GetFromId(Guid id)
        {
            return this.GetFromQueryable(this.commentFetcherService.GetById(id));
        }

        [ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Comment> queryable)
        {
            var model = queryable.Project().To<WallItemCommentViewModel>().FirstOrDefault();
            var state = this.commentFetcherService.CommentFlaggedByUser(Guid.Parse(model.Id), this.HttpContext.User.Identity.GetUserId());
            model.IsFlagged = state == null ? false : state.IsFlagged;

            return this.GetFromViewModel(model);
        }

        [ChildActionOnly]
        public ActionResult GetFromViewModel(WallItemCommentViewModel viewModel)
        {
            return this.PartialView(WallItemCommentPartialViewName, viewModel);
        }
    }
}