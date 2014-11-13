namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Services.Common.Logic.Reactions;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions on comments
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class CommentReactionsController : BaseController
    {
        private readonly ICommentReactionsService commentReactionsService;

        public CommentReactionsController(ICommentReactionsService commentReactionsService)
        {
            this.commentReactionsService = commentReactionsService;
        }

        [AjaxPost]              //contentId is the name of the ajax value
        public ActionResult Flag(Guid contentId)
        {
            return this.ConditionalActionResult<Guid>(() =>
                this.commentReactionsService.FlagComment(contentId, this.HttpContext.User.Identity.GetUserId()),
                (id) => this.PartialView(id));
        }
    }
}