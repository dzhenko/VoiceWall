namespace VoiceWall.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;
    using VoiceWall.Web.Areas.Administration.ViewModels;
    using VoiceWall.Web.Infrastructure.Caching;

    using Model = VoiceWall.Data.Models.Content;
    using ViewModel = VoiceWall.Web.Areas.Administration.ViewModels.ContentAdministrationViewModel;

    public class ContentsAdministrationController : KendoGridAdministrationController<Model, ViewModel>
    {
        private readonly ICacheService cache;

        public ContentsAdministrationController(IAdministrationService<Model> administrationService, ICacheService cache)
            : base(administrationService)
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

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy(model);
            this.cache.Clear(model.Id);
            return this.GridOperation(model, request);
        } 
    }
}