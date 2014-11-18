namespace VoiceWall.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;
    using VoiceWall.Web.Areas.Administration.ViewModels;
    using VoiceWall.Web.Infrastructure.Caching;

    using Model = VoiceWall.Data.Models.CommentView;
    using ViewModel = VoiceWall.Web.Areas.Administration.ViewModels.CommentViewAdministrationViewModel;

    public class CommentViewsAdministrationController : KendoGridAdministrationController<Model, ViewModel>
    {
        public CommentViewsAdministrationController(IAdministrationService<Model> administrationService)
            : base(administrationService)
        {
        }

        public ActionResult Index(Guid? commentId)
        {
            return View(commentId);
        }

        [HttpPost]
        public override ActionResult Read(DataSourceRequest request, Guid? commentId = null)
        {
            var data = this.AdministrationService.Read().AsQueryable();

            if (commentId.HasValue)
            {
                data = data.Where(c => c.CommentId == commentId.Value);
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