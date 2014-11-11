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

    public class WallItemHolderPartialController : BaseController
    {
        private const string PartialViewName = "_WallItemHolderPartial";

        public WallItemHolderPartialController(IVoiceWallData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult GetFromId(Guid id)
        {
            var query = this.Data.Contents.All().Where(c => c.Id == id);

            return this.GetFromQueryable(query);
        }

        [ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Content> queryable)
        {
            var content = queryable.Project().To<WallItemHolderViewModel>().FirstOrDefault();

            return this.GetFromViewModel(content);
        }

        [ChildActionOnly]
        public ActionResult GetFromViewModel(WallItemHolderViewModel viewModel)
        {
            Guid idAsGuid;
            if (!Guid.TryParse(viewModel.Id, out idAsGuid))
            {
                return this.HttpNotFound();
            }
            var userId = this.HttpContext.User.Identity.GetUserId();
            var state = this.Data.ContentViews.All().Where(cv => cv.UserId == userId && cv.ContentId == idAsGuid)
                .Select(cv => new { Liked = cv.Liked, Flagged = cv.Flagged }).FirstOrDefault();

            viewModel.IsLiked = state.Liked;
            viewModel.IsFlagged = state.Flagged;

            return this.PartialView(PartialViewName, viewModel);
        }
    }
}