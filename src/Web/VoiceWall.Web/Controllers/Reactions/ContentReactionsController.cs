namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using VoiceWall.Data;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions on content
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class ContentReactionsController : BaseController
    {
        public ContentReactionsController(IVoiceWallData data)
            : base(data)
        {
        }

        [AjaxPost]
        public ActionResult Like(Guid contentId)
        {
            return this.ConditionalActionResult(() => this.AlterViewLike(contentId, true), this.PartialView(contentId));
        }

        [AjaxPost]
        public ActionResult Hate(Guid contentId)
        {
            return this.ConditionalActionResult(() => this.AlterViewLike(contentId, false), this.PartialView(contentId));
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            return this.ConditionalActionResult(() => this.AlterFlagOnContent(contentId), this.PartialView(contentId));
        }

        // TODO: Service ?
        private void AlterViewLike(Guid contentId, bool like)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.Data.Contents.All().Where(c => c.Id == contentId)
                .Select(c => c.ContentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var content = this.Data.Contents.All().FirstOrDefault(c => c.Id == contentId);

                if (content == null)
                {
                    throw new ArgumentException("Content does not exist");
                }

                content.ContentViews.Add(new ContentView()
                {
                    Liked = like,
                    UserId = userId
                });
            }
            else
            {
                // if they have the same value they are nulled
                if (like == view.Liked)
                {
                    view.Liked = null;
                }
                else
                {
                    view.Liked = like;
                }
            }

            this.Data.Contents.SaveChanges();
        }

        private void AlterFlagOnContent(Guid contentId)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.Data.Contents.All().Where(c => c.Id == contentId).Select(c => c.ContentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var content = this.Data.Contents.All().FirstOrDefault(c => c.Id == contentId);

                if (content == null)
                {
                    throw new ArgumentException("Content does not exist");
                }

                content.ContentViews.Add(new ContentView()
                {
                    Flagged = true,
                    UserId = userId
                });
            }
            else
            {
                view.Flagged = !view.Flagged;
            }

            this.Data.Contents.SaveChanges();
        }
    }
}