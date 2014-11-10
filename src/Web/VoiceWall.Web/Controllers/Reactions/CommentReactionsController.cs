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
    /// Used as an endpoint for ajax requests for reactions on comments
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class CommentReactionsController : BaseContentAndCommentController
    {
        public CommentReactionsController(IDeletableEntityRepository<Content> contentsRepository,
            IDeletableEntityRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
        }

        [AjaxPost]
        public ActionResult Flag(Guid contentId)
        {
            var success = this.AlterFlagOnComment(contentId);

            if (!success)
            {
                return this.HttpNotFound();
            }

            return this.GetWallItemComment(contentId);
        }

        private bool AlterFlagOnComment(Guid commentId)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.CommentsRepository.All().Where(c => c.Id == commentId).Select(c => c.CommentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var comment = this.CommentsRepository.All().FirstOrDefault(c => c.Id == commentId);

                if (comment == null)
                {
                    return false;
                }

                comment.CommentViews.Add(new CommentView()
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
                this.CommentsRepository.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private ActionResult GetWallItemComment(Guid commentId)
        {
            return null;
        }
    }
}