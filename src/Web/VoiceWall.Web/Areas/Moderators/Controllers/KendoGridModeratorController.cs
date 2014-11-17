namespace VoiceWall.Web.Areas.Moderators.Controllers
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data.Common.Models;
    using VoiceWall.Web.Areas.Moderators.ViewModels;
    using VoiceWall.Web.Infrastructure.Caching;
    using VoiceWall.Services.Common.Moderation;

    public abstract class KendoGridModeratorController<TDbModel, TViewModel> : ModeratorController 
        where TDbModel : IDeletableEntity
        where TViewModel : ModerationViewModel
    {
        private readonly IModerationService<TDbModel> moderationService;

        public KendoGridModeratorController(IModerationService<TDbModel> moderationService, ICacheService cache)
            : base(cache)
        {
            this.moderationService = moderationService;
        }

        protected IModerationService<TDbModel> ModerationService
        {
            get { return this.moderationService; }
        }

        [HttpPost]
        public virtual ActionResult Read([DataSourceRequest]DataSourceRequest request, Guid? id = null)
        {
            var data = this.moderationService.Read().AsQueryable().Project().To<TViewModel>().ToDataSourceResult(request);
            return this.Json(data);
        }

        [NonAction]
        protected virtual void Update(TViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.moderationService.ChangeHide(model.Id, model.IsHidden);
                this.Cache.Clear(model.Id);
            }
        }

        protected JsonResult GridOperation(TViewModel model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}