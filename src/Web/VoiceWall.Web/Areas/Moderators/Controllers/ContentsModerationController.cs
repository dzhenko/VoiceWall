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
        private readonly ICacheService cache;

        public ContentsModerationController(IModerationService<Model> moderationService, ICacheService cache)
            : base(moderationService)
        {
            this.cache = cache;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update(model);
            this.cache.Clear(model.Id);
            return this.GridOperation(model, request);
        }
    }
}