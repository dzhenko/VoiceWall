namespace VoiceWall.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data;
    using VoiceWall.Web.ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(IVoiceWallData data)
            : base(data)
        {
        }

        [Authorize]
        [OutputCache(Duration = 10, VaryByCustom = "User")]
        public ActionResult Index()
        {
            return View(this.Data.Contents.All()
                .OrderByDescending(c => c.CreatedOn).Take(10).Project().To<WallItemViewModel>());
        }
    }
}