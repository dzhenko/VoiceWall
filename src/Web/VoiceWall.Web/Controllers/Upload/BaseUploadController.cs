namespace VoiceWall.Web.Controllers.Upload
{
    using System;
    using System.Web;

    using Microsoft.AspNet.Identity;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

    /// <summary>
    /// Abstract controller used to provide creation + upload methods
    /// </summary>

    public abstract class BaseUploadController : BaseContentAndCommentController
    {
        private readonly ICloudStorage cloudStorage;
        public BaseUploadController(ICloudStorage cloudStorage,
            IDeletableEntityRepository<Content> contentsRepository, IDeletableEntityRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
            this.cloudStorage = cloudStorage;
        }

        protected ICloudStorage CloudStorage
        {
            get { return this.cloudStorage; }
        }

        protected Guid CreateContent(HttpPostedFileBase file, ContentType type)
        {
            var content = new Content()
            {
                ContentType = type,
                UserId = this.HttpContext.User.Identity.GetUserId()
            };

            content.ContentUrl = this.cloudStorage.UploadFile(file.InputStream, content.Id.ToString(), file.ContentType);

            this.ContentsRepository.Add(content);
            this.ContentsRepository.SaveChanges();

            return content.Id;
        }

        protected Guid CreateComment(HttpPostedFileBase file, ContentType type, Guid contentId)
        {
            var comment = new Comment()
            {
                ContentType = type,
                UserId = this.HttpContext.User.Identity.GetUserId(),
                ContentId = contentId
            };

            comment.ContentUrl = this.cloudStorage.UploadFile(file.InputStream, comment.Id.ToString(), file.ContentType);

            this.CommentsRepository.Add(comment);
            this.CommentsRepository.SaveChanges();

            return comment.Id;
        }
    }
}