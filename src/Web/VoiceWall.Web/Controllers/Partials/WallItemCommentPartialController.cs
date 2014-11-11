namespace VoiceWall.Web.Controllers.Partials
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using VoiceWall.Data;
    using VoiceWall.Web.ViewModels;
    using VoiceWall.Data.Models;

    public class WallItemCommentPartialController : BaseController
    {
        private const string WallItemCommentPartialViewName = "_WallItemCommentPartial";

        public WallItemCommentPartialController(IVoiceWallData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult GetFromId(Guid id)
        {
            var query = this.Data.Comments.All().Where(c => c.Id == id);

            return this.GetFromQueryable(query);
        }

        [ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Comment> queryable)
        {
            var comment = queryable.Project().To<WallItemCommentViewModel>().FirstOrDefault();

            return this.GetFromViewModel(comment);
        }

        [ChildActionOnly]
        private ActionResult GetFromViewModel(WallItemCommentViewModel viewModel)
        {
            Guid idAsGuid;
            if (!Guid.TryParse(viewModel.Id, out idAsGuid) || idAsGuid == Guid.Empty)
            {
                return this.HttpNotFound();
            }

            var userId = this.HttpContext.User.Identity.GetUserId();

            viewModel.IsFlagged = this.Data.CommentViews.All()
                .FirstOrDefault(cv => cv.UserId == userId && cv.CommentId == idAsGuid).Flagged;

            return this.PartialView(WallItemCommentPartialViewName, viewModel);
        }
    }
}