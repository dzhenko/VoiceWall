namespace VoiceWall.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data.Common.Models;
    using VoiceWall.Web.Areas.Administration.ViewModels;
    using VoiceWall.Services.Common.Administration;
    using VoiceWall.Web.Infrastructure.Caching;
    using System;

    public abstract class KendoGridAdministrationController<TDbModel, TViewModel> : AdminController 
        where TDbModel : IDeletableEntity
        where TViewModel : AdministrationViewModel
    {
        private readonly IAdministrationService<TDbModel> administrationService;

        public KendoGridAdministrationController(IAdministrationService<TDbModel> administrationService, ICacheService cache)
            : base(cache)
        {
            this.administrationService = administrationService;
        }

        protected IAdministrationService<TDbModel> AdministrationService
        {
            get { return this.administrationService; }
        }

        [HttpPost]
        public virtual ActionResult Read([DataSourceRequest]DataSourceRequest request, Guid? id = null)
        {
            var data = this.administrationService.Read().AsQueryable().Project().To<TViewModel>().ToDataSourceResult(request);
            return this.Json(data);
        }

        [NonAction]
        protected virtual void Update([Bind(Exclude="ModifiedOn")]TViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.administrationService.Get(model.Id);
                Mapper.Map<TViewModel, TDbModel>(model, dbModel);
                this.administrationService.Update(dbModel);

                this.Cache.Clear(model.Id);
            }
        }

        [NonAction]
        protected virtual void Destroy(object id)
        {
            this.administrationService.Delete(id);
            this.Cache.Clear(id);
        }

        [NonAction]
        protected virtual void Destroy(TViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.administrationService.Delete(model.Id);
                this.Cache.Clear(model.Id);
            }
        }

        protected JsonResult GridOperation(TViewModel model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}