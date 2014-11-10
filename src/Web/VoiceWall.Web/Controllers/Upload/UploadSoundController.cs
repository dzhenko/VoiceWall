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

    public class UploadSoundController : BaseUploadController
    {
        private ICloudStorage storage;

        public UploadSoundController(ISoundsCloudStorage soundsCloudStorageProvider, IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
            this.storage = soundsCloudStorageProvider;
        }

        public ActionResult Create(NewSoundContentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            //var sound = new Content()
            //{
            //    ContentType = ContentType.Sound,
            //    UserId = this.HttpContext.User.Identity.GetUserId()
            //};

            //var url = this.storage.UploadFile(model.File.InputStream, sound.Id.ToString(), model.File.ContentType);

            //sound.ContentUrl = url;

            //this.ContentsRepository.Add(sound);
            //this.ContentsRepository.SaveChanges();

            var soundId = this.CreateContent(this.storage, model.File, ContentType.Sound);

            // projecting only the holder - the comments are empty as we just created the item
            var viewModelHolder = this.ContentsRepository.All()
                                .Where(c => c.Id == soundId)
                                .Project()
                                .To<WallItemHolderViewModel>()
                                .FirstOrDefault();

            return this.PartialView("__WallItemPartial", new WallItemViewModel() { WallItemHolderViewModel = viewModelHolder });
        }

        public ActionResult Comment(NewSoundCommentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            if (!this.ContentsRepository.All().Any(c => c.Id == model.ContentId))
            {
                return this.Json(new { message = "Content not found" });
            }

            //var sound = new Comment()
            //{
            //    ContentType = ContentType.Sound,
            //    UserId = this.HttpContext.User.Identity.GetUserId(),
            //    ContentId = model.ContentId
            //};

            //var url = this.storage.UploadFile(model.File.InputStream, sound.Id.ToString(), model.File.ContentType);

            //sound.ContentUrl = url;

            //this.CommentsRepository.Add(sound);
            //this.CommentsRepository.SaveChanges();

            var soundId = this.CreateComment(this.storage, model.File, ContentType.Sound, model.ContentId);

            var viewModel = this.CommentsRepository.All()
                                .Where(c => c.Id == soundId)
                                .Project()
                                .To<WallItemCommentViewModel>()
                                .FirstOrDefault();

            return this.PartialView("_WallItemCommentPartial", viewModel);
        }
    }
}