namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

    public abstract class BaseContentAndCommentController : Controller
    {
        private readonly IRepository<Content> contentsRepository;
        private readonly IRepository<Comment> commentsRepository;

        public BaseContentAndCommentController(IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
        {
            this.contentsRepository = contentsRepository;
            this.commentsRepository = commentsRepository;
        }
        
        protected IRepository<Content> ContentsRepository
        {
            get { return this.contentsRepository; }
        }

        protected IRepository<Comment> CommentsRepository
        {
            get { return this.commentsRepository; }
        }
    }
}