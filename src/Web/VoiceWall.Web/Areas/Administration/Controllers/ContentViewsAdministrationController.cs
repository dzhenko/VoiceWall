namespace VoiceWall.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;
    using VoiceWall.Web.Areas.Administration.ViewModels;
    using VoiceWall.Web.Infrastructure.Caching;

    using Model = VoiceWall.Data.Models.ContentView;
    using ViewModel = VoiceWall.Web.Areas.Administration.ViewModels.ContentViewAdministrationViewModel;

    public class ContentViewsAdministrationController : KendoGridAdministrationController<Model, ViewModel>
    {
        public ContentViewsAdministrationController(IAdministrationService<Model> administrationService)
            : base(administrationService)
        {
        }

        public ActionResult Index(Guid? contentId)
        {
            return View(contentId);
        }

        [HttpPost]
        public override ActionResult Read(DataSourceRequest request, Guid? contentId = null)
        {
            var data = this.AdministrationService.Read().AsQueryable();

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
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy(model);
            return this.GridOperation(model, request);
        } 
    }
}