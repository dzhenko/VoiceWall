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
    /// Used as an endpoint for ajax requests for uploading picture content and returns partials of the updated/created content.
    /// </summary>

    [Authorize]
    [ValidateAntiForgeryToken]
    public class UploadPictureController : BaseUploadController
    {
        public UploadPictureController(IPicturesCloudStorage picturesCloudStorageProvider, IVoiceWallData data)
            : base(picturesCloudStorageProvider, data)
        {
        }

        [AjaxPost]
        public ActionResult Create(NewPictureContentInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            var pictureId = this.CreateContent(model.File, ContentType.Picture);

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.Data.Contents.All()
                                .Where(c => c.Id == pictureId)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

        [AjaxPost]
        public ActionResult Comment(NewPictureCommentInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            if (!this.Data.Contents.All().Any(c => c.Id == model.ContentId))
            {
                return this.HttpNotFound();
            }

            var pictureCommentId = this.CreateComment(model.File, ContentType.Picture, model.ContentId);

            var viewModel = this.Data.Comments.All()
                                .Where(c => c.Id == pictureCommentId)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}