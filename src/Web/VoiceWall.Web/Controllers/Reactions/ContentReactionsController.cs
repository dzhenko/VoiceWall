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
            var success = this.AlterViewLike(contentId, true);

            if (!success)
            {
                return this.HttpNotFound();
            }

            return this.RedirectToAction("GetFromId", "WallItemHolderPartial", new { id = contentId });
        }

        [AjaxPost]
        public ActionResult Hate(Guid contentId)
        {
            var success = this.AlterViewLike(contentId, false);

            if (!success)
            {
                return this.HttpNotFound();
            }

            return this.RedirectToAction("GetFromId", "WallItemHolderPartial", new { id = contentId });
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            var success = this.AlterFlagOnContent(contentId);

            if (!success)
            {
                return this.HttpNotFound();
            }

            return this.RedirectToAction("GetFromId", "WallItemHolderPartial", new { id = contentId });
        }

        // TODO: Service ?
        private bool AlterViewLike(Guid contentId, bool like)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.Data.Contents.All().Where(c => c.Id == contentId)
                .Select(c => c.ContentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var content = this.Data.Contents.All().FirstOrDefault(c => c.Id == contentId);

                if (content == null)
                {
                    return false;
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
                if (like == view.Liked == true)
                {
                    view.Liked = null;
                }
                else
                {
                    view.Liked = like;
                }
            }

            try
            {
                this.Data.Contents.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool AlterFlagOnContent(Guid contentId)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.Data.Contents.All().Where(c => c.Id == contentId).Select(c => c.ContentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var content = this.Data.Contents.All().FirstOrDefault(c => c.Id == contentId);

                if (content == null)
                {
                    return false;
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

            try
            {
                this.Data.Contents.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}