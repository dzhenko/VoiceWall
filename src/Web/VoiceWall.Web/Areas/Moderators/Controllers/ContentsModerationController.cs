namespace VoiceWall.Web.Areas.Moderators.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Moderation;
    using VoiceWall.Web.Areas.Administration.ViewModels;
    using VoiceWall.Web.Infrastructure.Caching;

    using Model = VoiceWall.Data.Models.Content;
    using ViewModel = VoiceWall.Web.Areas.Moderators.ViewModels.ContentModerationViewModel;

    public class ContentsModerationController : KendoGridModeratorController<Model, ViewModel>
    {
        public ContentsModerationController(IModerationService<Model> moderationService, ICacheService cache)
            : base(moderationService, cache)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update(model);
            this.Cache.Clear(model.Id);
            return this.GridOperation(model, request);
        }
    }
}