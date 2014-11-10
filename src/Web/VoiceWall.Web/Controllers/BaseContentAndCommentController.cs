namespace VoiceWall.Web.Controllers
{
    using System.Web.Mvc;

    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

    /// <summary>
    /// Abstract controller used to provide content and comments repositories to its successors.
    /// </summary>

    public abstract class BaseContentAndCommentController : Controller
    {
        private readonly IDeletableEntityRepository<Content> contentsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public BaseContentAndCommentController(IDeletableEntityRepository<Content> contentsRepository, IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.contentsRepository = contentsRepository;
            this.commentsRepository = commentsRepository;
        }

        protected IDeletableEntityRepository<Content> ContentsRepository
        {
            get { return this.contentsRepository; }
        }

        protected IDeletableEntityRepository<Comment> CommentsRepository
        {
            get { return this.commentsRepository; }
        }
    }
}