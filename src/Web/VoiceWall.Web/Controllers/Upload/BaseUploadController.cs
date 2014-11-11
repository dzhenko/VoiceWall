namespace VoiceWall.Web.Controllers.Upload
{
    using System;
    using System.Web;

    using Microsoft.AspNet.Identity;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Models;
    using VoiceWall.Data;

    /// <summary>
    /// Abstract controller used to provide creation + upload methods
    /// </summary>

    public abstract class BaseUploadController : BaseController
    {
        private readonly ICloudStorage cloudStorage;
        public BaseUploadController(ICloudStorage cloudStorage, IVoiceWallData data)
            : base(data)
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

            this.Data.Contents.Add(content);
            this.Data.SaveChanges();

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

            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();

            return comment.Id;
        }
    }
}