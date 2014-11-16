namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Web.Infrastructure.Caching;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Services.Common.Logic.Reactions;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions on content
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class ContentReactionsController : BaseReactionsController
    {
        private readonly IContentReactionsService contentReactionsService;

        public ContentReactionsController(IContentReactionsService contentReactionsService, ICacheService cache)
            : base(cache)
        {
            this.contentReactionsService = contentReactionsService;
        }

        [AjaxPost]
        public ActionResult Like(Guid contentId)
        {
            return this.ConditionalCacheRemovingActionResult(contentId,
                    () => this.contentReactionsService.LikeComment(contentId, this.HttpContext.User.Identity.GetUserId()),
                    this.PartialView(contentId));
        }

        [AjaxPost]
        public ActionResult Hate(Guid contentId)
        {
            return this.ConditionalCacheRemovingActionResult(contentId,
                    () => this.contentReactionsService.HateComment(contentId, this.HttpContext.User.Identity.GetUserId()),
                    this.PartialView(contentId));
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            return this.ConditionalCacheRemovingActionResult(contentId,
                    () => this.contentReactionsService.FlagContent(contentId, this.HttpContext.User.Identity.GetUserId()),
                    this.PartialView(contentId));
        }
    }
}