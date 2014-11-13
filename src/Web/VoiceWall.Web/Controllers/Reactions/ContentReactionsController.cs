namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Services.Common.Logic.Reactions;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions on content
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class ContentReactionsController : BaseController
    {
        private readonly IContentReactionsService contentReactionsService;

        public ContentReactionsController(IContentReactionsService contentReactionsService)
        {
            this.contentReactionsService = contentReactionsService;
        }

        [AjaxPost]
        public ActionResult Like(Guid contentId)
        {
            return this.ConditionalActionResult<Guid>(() =>
                this.contentReactionsService.LikeComment(contentId, this.HttpContext.User.Identity.GetUserId()),
                (id) => this.PartialView(id));
        }

        [AjaxPost]
        public ActionResult Hate(Guid contentId)
        {
            return this.ConditionalActionResult<Guid>(() =>
                this.contentReactionsService.HateComment(contentId, this.HttpContext.User.Identity.GetUserId()),
                (id) => this.PartialView(id));
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            return this.ConditionalActionResult<Guid>(() =>
                this.contentReactionsService.FlagContent(contentId, this.HttpContext.User.Identity.GetUserId()),
                (id) => this.PartialView(id));
        }
    }
}