namespace VoiceWall.Web.Controllers.Upload
{
    using System.Web.Mvc;

    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

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
    }
}