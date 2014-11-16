namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using VoiceWall.Services.Common.Generators;
    using VoiceWall.Web.Infrastructure.Caching;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;

    /// <summary>
    /// Used as an endpoint for ajax requests for uploading sound content and returns partials of the updated/created content.
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class UploadSoundController : BaseUploadController
    {
        public UploadSoundController(ISoundUploadingGeneratorService pictureUploadingGeneratorService, ICacheService cache)
            : base(pictureUploadingGeneratorService, cache)
        {
        }

        [AjaxPost]
        public ActionResult Create(NewSoundContentInputModel model)
        {
            return this.ConditionalActionResult<Guid>(() => this.CreateContent(model.File), 
                                                      (id) => this.PartialView(id));
        }

        [AjaxPost]
        public ActionResult Comment(NewSoundCommentInputModel model)
        {
            return this.ConditionalActionResult<Guid>(() => this.CommentContent(model.File, model.ContentId), 
                                                      (id) => this.PartialView(id));
        }
    }
}