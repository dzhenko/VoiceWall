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

    public class WallItemPartialController: BaseController
    {
        private const string PartialViewName = "_WallItemPartial";

        public WallItemPartialController(IVoiceWallData data)
            : base(data)
        {
        }

        //[ChildActionOnly]
        public ActionResult GetFromId(Guid id)
        {
            var query = this.Data.Contents.All().Where(c => c.Id == id);

            return this.GetFromQueryable(query);
        }

        //[ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Content> queryable)
        {
            var content = queryable.Project().To<WallItemViewModel>().FirstOrDefault();

            return this.GetFromViewModel(content);
        }

        //[ChildActionOnly]
        public ActionResult GetFromViewModel(WallItemViewModel viewModel)
        {
            return this.PartialView(PartialViewName, viewModel);
        }
    }
}