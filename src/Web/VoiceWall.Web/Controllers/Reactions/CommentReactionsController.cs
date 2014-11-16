namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Web.Infrastructure.Caching;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Services.Common.Logic.Reactions;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions on comments
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class CommentReactionsController : BaseReactionsController
    {
        private readonly ICommentReactionsService commentReactionsService;

        public CommentReactionsController(ICommentReactionsService commentReactionsService, ICacheService cache)
            : base(cache)
        {
            this.commentReactionsService = commentReactionsService;
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            return this.ConditionalCacheRemovingActionResult(contentId,
                    () => this.commentReactionsService.FlagComment(contentId, this.HttpContext.User.Identity.GetUserId()),
                    this.PartialView(contentId));
        }
    }
}