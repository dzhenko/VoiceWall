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

    using Model = VoiceWall.Data.Models.User;
    using ViewModel = VoiceWall.Web.Areas.Administration.ViewModels.UsersAdministrationViewModel;

    public class UsersAdministrationController : AdminController
    {
        private readonly IUserAdministrationService userAdministrationService;
        private readonly ICacheService cache;

        public UsersAdministrationController(IUserAdministrationService userAdministrationService, ICacheService cache)
        {
            this.userAdministrationService = userAdministrationService;
            this.cache = cache;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = this.userAdministrationService
                .Read()
                .AsQueryable()
                .Project()
                .To<ViewModel>()
                .ToDataSourceResult(request);

            return this.Json(data);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var allInfluencedContent = this.userAdministrationService.UpdateUser(model.Id, model.IsCurrentlyAdmin, model.IsCurrentlyModerator, model.IsHidden);

            foreach (var item in allInfluencedContent)
            {
                this.cache.Clear(item);
            }
            
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var allInfluencedContent = this.userAdministrationService.DeleteUser(model.Id);

            foreach (var item in allInfluencedContent)
            {
                this.cache.Clear(item);
            }

            return this.GridOperation(model, request);
        }

        protected JsonResult GridOperation(ViewModel model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}