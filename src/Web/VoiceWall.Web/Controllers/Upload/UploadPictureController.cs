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

    public class UploadPictureController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadPictureController(IPicturesCloudStorage picturesCloudStorageProvider, IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
            this.storage = picturesCloudStorageProvider;
        }

        [AjaxPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewPictureContentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            var picture = new Content()
            {
                ContentType = ContentType.Picture,
                UserId = this.HttpContext.User.Identity.GetUserId()
            };

            var url = this.storage.UploadFile(model.File.InputStream, picture.Id.ToString(), model.File.ContentType);

            picture.ContentUrl = url;

            this.ContentsRepository.Add(picture);
            this.ContentsRepository.SaveChanges();

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.ContentsRepository.All()
                                .Where(c => c.Id == picture.Id)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

        [AjaxPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(NewPictureCommentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            if (!this.ContentsRepository.All().Any(c => c.Id == model.ContentId))
            {
                return this.Json(new { message = "Content not found" });
            }

            var comment = new Comment()
            {
                ContentType = ContentType.Picture,
                UserId = this.HttpContext.User.Identity.GetUserId(),
                ContentId = model.ContentId
            };

            var url = this.storage.UploadFile(model.File.InputStream, comment.Id.ToString(), model.File.ContentType);

            comment.ContentUrl = url;

            this.CommentsRepository.Add(comment);
            this.CommentsRepository.SaveChanges();

            var viewModel = this.CommentsRepository.All()
                                .Where(c => c.Id == comment.Id)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}