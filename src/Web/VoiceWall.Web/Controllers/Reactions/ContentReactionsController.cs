namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions on content
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class ContentReactionsController : BaseContentAndCommentController
    {
        public ContentReactionsController(IDeletableEntityRepository<Content> contentsRepository,
            IDeletableEntityRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
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

            return this.GetWallItemHolder(contentId);
        }

        [AjaxPost]
        public ActionResult Hate(Guid contentId)
        {
            var success = this.AlterViewLike(contentId, false);

            if (!success)
            {
                return this.HttpNotFound();
            }

            return this.GetWallItemHolder(contentId);
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            var success = this.AlterFlagOnContent(contentId);

            if (!success)
            {
                return this.HttpNotFound();
            }

            return this.GetWallItemHolder(contentId);
        }

        private bool AlterViewLike(Guid contentId, bool like)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.ContentsRepository.All().Where(c => c.Id == contentId)
                .Select(c => c.ContentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var content = this.ContentsRepository.All().FirstOrDefault(c => c.Id == contentId);

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
                this.ContentsRepository.SaveChanges();
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
            var view = this.ContentsRepository.All().Where(c => c.Id == contentId).Select(c => c.ContentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var content = this.ContentsRepository.All().FirstOrDefault(c => c.Id == contentId);

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
                this.ContentsRepository.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private ActionResult GetWallItemHolder(Guid wallItemId)
        {
            return null;
        }
    }
}