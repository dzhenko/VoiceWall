namespace VoiceWall.Web.Areas.Moderators.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Moderation;
    using VoiceWall.Web.Areas.Administration.ViewModels;
    using VoiceWall.Web.Infrastructure.Caching;

    using Model = VoiceWall.Data.Models.Comment;
    using ViewModel = VoiceWall.Web.Areas.Moderators.ViewModels.CommentModerationViewModel;

    public class CommentsModerationController : KendoGridModeratorController<Model, ViewModel>
    {
        private readonly ICacheService cache;

        public CommentsModerationController(IModerationService<Model> moderationService, ICacheService cache)
            : base(moderationService)
        {
            this.cache = cache;
        }

        public ActionResult Index(Guid? contentId)
        {
            return View(contentId);
        }

        [HttpPost]
        public override ActionResult Read(DataSourceRequest request, Guid? contentId = null)
        {
            var data = this.ModerationService.Read().AsQueryable();

            if (contentId.HasValue)
            {
                data = data.Where(c => c.ContentId == contentId.Value);
            }

            return this.Json(data.Project().To<ViewModel>().ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update(model);
            this.cache.Clear(model);
            return this.GridOperation(model, request);
        }
    }
}