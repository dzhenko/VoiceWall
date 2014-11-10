namespace VoiceWall.Web.Controllers.Upload
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;
    using VoiceWall.Web.ViewModels.Upload;
    using VoiceWall.Web.ViewModels;

    [Authorize]
    [ValidateAntiForgeryToken]
    public class UploadVideoController : BaseUploadController
    {
        public UploadVideoController(IVideosCloudStorage videosCloudStorageProvider, IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
            : base(videosCloudStorageProvider, contentsRepository, commentsRepository)
        {
        }

        [AjaxPost]
        public ActionResult Create(NewVideoContentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            var videoId = this.CreateContent(model.File, ContentType.Video);

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.ContentsRepository.All()
                                .Where(c => c.Id == videoId)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

        [AjaxPost]
        public ActionResult Comment(NewVideoCommentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            if (!this.ContentsRepository.All().Any(c => c.Id == model.ContentId))
            {
                return this.Json(new { message = "Content not found" });
            }

            var videoCommentId = this.CreateComment(model.File, ContentType.Video, model.ContentId);

            var viewModel = this.CommentsRepository.All()
                                .Where(c => c.Id == videoCommentId)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}