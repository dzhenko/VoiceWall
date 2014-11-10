namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.ViewModels;

    public class HomeController : BaseContentAndCommentController
    {
        public HomeController(IDeletableEntityRepository<Content> contentsRepository, IDeletableEntityRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
        }

        [Authorize]
        //[OutputCache(Duration = 10, VaryByCustom = "User")]
        public ActionResult Index()
        {
            return View(this.ContentsRepository.All()
                .OrderByDescending(c => c.CreatedOn).Take(10).Project().To<WallItemViewModel>());
        }
    }
}