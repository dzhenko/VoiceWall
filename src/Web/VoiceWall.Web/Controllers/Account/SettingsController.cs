namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using VoiceWall.Services.Common.Users;
    using VoiceWall.Web.ViewModels.Account;

    [Authorize]
    public class SettingsController : BaseController
    {
        private IOwnProfileService ownProfileService;

        public SettingsController(IOwnProfileService ownProfileService)
        {
            this.ownProfileService = ownProfileService;
        }

        [HttpGet]
        public ActionResult General()
        {
            return this.ConditionalActionResult(() => 
                this.ownProfileService.GetUserProfile(this.HttpContext.User.Identity.GetUserId())
                    .Project().To<SingleProfileViewModel>().FirstOrDefault(),
                (profile) => this.View(profile));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UpdateProfileViewModel model)
        {
            return this.IndependentActionResult(() => 
                this.ownProfileService.ChangeUserPicture(User.Identity.GetUserId(), model.File.InputStream, model.File.ContentType),
                this.RedirectToAction<SettingsController>((c) => c.General()));
        }
    }
}