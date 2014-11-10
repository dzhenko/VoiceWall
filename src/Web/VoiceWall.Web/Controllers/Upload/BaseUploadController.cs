namespace VoiceWall.Web.Controllers.Upload
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;

    [Authorize]
    [ValidateAntiForgeryToken]
    public abstract class BaseUploadController : Controller
    {
        private readonly IRepository<Content> contentsRepo;
        private readonly IRepository<Comment> commentsRepo;

        public BaseUploadController(IRepository<Content> contentsRepo, IRepository<Comment> commentsRepo)
        {
            this.contentsRepo = contentsRepo;
            this.commentsRepo = commentsRepo;
        }

        protected IRepository<Content> ContentsRepository
        {
            get { return this.contentsRepo; }
        }

        protected IRepository<Comment> CommentsRepository
        {
            get { return this.commentsRepo; }
        }

        [AjaxPost]
        protected Guid CreateContent(ICloudStorage storage, HttpPostedFileBase file, ContentType type)
        {
            var content = new Content()
            {
                ContentType = type,
                UserId = this.HttpContext.User.Identity.GetUserId()
            };

            content.ContentUrl = storage.UploadFile(file.InputStream, content.Id.ToString(), file.ContentType);

            this.ContentsRepository.Add(content);
            this.ContentsRepository.SaveChanges();

            return content.Id;
        }

        [AjaxPost]
        protected Guid CreateComment(ICloudStorage storage, HttpPostedFileBase file, ContentType type, Guid contentId)
        {
            var comment = new Comment()
            {
                ContentType = type,
                UserId = this.HttpContext.User.Identity.GetUserId(),
                ContentId = contentId
            };

            comment.ContentUrl = storage.UploadFile(file.InputStream, comment.Id.ToString(), file.ContentType);

            this.CommentsRepository.Add(comment);
            this.CommentsRepository.SaveChanges();

            return comment.Id;
        }
    }
}