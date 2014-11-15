namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    
    using VoiceWall.Services.Common.Users;
    using VoiceWall.Web.ViewModels.Profile;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels;

    public class ProfileController : BaseController
    {
        private IUserProfileService userProfileService;

        public ProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }


        public ActionResult Details(Guid id)
        {
            return this.ConditionalActionResult<ProfileDetailsViewModel>(
                () => this.userProfileService.GetUserProfile(id.ToString())
                    .Project().To<ProfileDetailsViewModel>().FirstOrDefault(),
                (profile) => this.View(profile));
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult More(int id, Guid username)
        {
            return this.ConditionalActionResult(
                () => this.userProfileService.GetNext(username.ToString(), id).Project().To<WallItemViewModel>(),
                (wallItems) => this.PartialView("_WallItemMultiplePartials", wallItems));
        }
    }
}