namespace VoiceWall.Web.Controllers.Upload
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;
    using VoiceWall.Web.ViewModels;

    /// <summary>
    /// Used as an endpoint for ajax requests for uploading video content and returns partials of the updated/created content.
    /// </summary>
    
    [Authorize]
    [ValidateAntiForgeryToken]
    public class UploadVideoController : BaseUploadController
    {
        public UploadVideoController(IVideosCloudStorage videosCloudStorageProvider, IVoiceWallData data)
            : base(videosCloudStorageProvider, data)
        {
        }

        [AjaxPost]
        public ActionResult Create(NewVideoContentInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            var videoId = this.CreateContent(model.File, ContentType.Video);

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.Data.Contents.All()
                                .Where(c => c.Id == videoId)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

        [AjaxPost]
        public ActionResult Comment(NewVideoCommentInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            if (!this.Data.Contents.All().Any(c => c.Id == model.ContentId))
            {
                return this.HttpNotFound();
            }

            var videoCommentId = this.CreateComment(model.File, ContentType.Video, model.ContentId);

            var viewModel = this.Data.Comments.All()
                                .Where(c => c.Id == videoCommentId)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}