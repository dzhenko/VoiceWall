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

    public class UploadVideoController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadVideoController(IVideosCloudStorage videosCloudStorageProvider, IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
            this.storage = videosCloudStorageProvider;
        }

        public ActionResult Create(NewVideoContentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            //var video = new Content()
            //{
            //    ContentType = ContentType.Video,
            //    UserId = this.HttpContext.User.Identity.GetUserId()
            //};

            //var url = this.storage.UploadFile(model.File.InputStream, video.Id.ToString(), model.File.ContentType);

            //video.ContentUrl = url;

            //this.ContentsRepository.Add(video);
            //this.ContentsRepository.SaveChanges();

            var videoId = this.CreateContent(this.storage, model.File, ContentType.Video);

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.ContentsRepository.All()
                                .Where(c => c.Id == videoId)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

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

            //var video = new Comment()
            //{
            //    ContentType = ContentType.Video,
            //    UserId = this.HttpContext.User.Identity.GetUserId(),
            //    ContentId = model.ContentId
            //};

            //var url = this.storage.UploadFile(model.File.InputStream, video.Id.ToString(), model.File.ContentType);

            //video.ContentUrl = url;

            //this.CommentsRepository.Add(video);
            //this.CommentsRepository.SaveChanges();

            var videoCommentId = this.CreateComment(this.storage, model.File, ContentType.Video, model.ContentId);

            var viewModel = this.CommentsRepository.All()
                                .Where(c => c.Id == videoCommentId)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}