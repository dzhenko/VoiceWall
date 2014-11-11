namespace VoiceWall.Web.Controllers.Upload
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;
    using VoiceWall.Web.ViewModels;
    using VoiceWall.Data;

    /// <summary>
    /// Used as an endpoint for ajax requests for uploading sound content and returns partials of the updated/created content.
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class UploadSoundController : BaseUploadController
    {
        public UploadSoundController(ISoundsCloudStorage soundsCloudStorageProvider, IVoiceWallData data)
            : base(soundsCloudStorageProvider, data)
        {
        }

        [AjaxPost]
        public ActionResult Create(NewSoundContentInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            var soundId = this.CreateContent(model.File, ContentType.Sound);

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.Data.Contents.All()
                                .Where(c => c.Id == soundId)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

        [AjaxPost]
        public ActionResult Comment(NewSoundCommentInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            if (!this.Data.Contents.All().Any(c => c.Id == model.ContentId))
            {
                return this.HttpNotFound();
            }

            var soundId = this.CreateComment(model.File, ContentType.Sound, model.ContentId);

            var viewModel = this.Data.Comments.All()
                                .Where(c => c.Id == soundId)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}