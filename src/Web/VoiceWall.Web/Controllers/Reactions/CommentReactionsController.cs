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
    /// Used as an endpoint for ajax requests for reactions on comments
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class CommentReactionsController : BaseController
    {
        public CommentReactionsController(IVoiceWallData data)
            : base(data)
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

            return this.RedirectToAction("GetFromId", "WallItemCommentPartial", new { id = contentId });
        }

        //TODO: Service
        private bool AlterFlagOnComment(Guid commentId)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var view = this.Data.Comments.All().Where(c => c.Id == commentId).Select(c => c.CommentViews.FirstOrDefault(v => v.UserId == userId)).FirstOrDefault();

            if (view == null)
            {
                var comment = this.Data.Comments.All().FirstOrDefault(c => c.Id == commentId);

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
                this.Data.Comments.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}