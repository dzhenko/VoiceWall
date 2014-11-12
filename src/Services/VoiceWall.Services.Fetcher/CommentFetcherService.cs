namespace VoiceWall.Services.Fetcher
{
    using System;
    using System.Linq;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;

    public class CommentFetcherService : ICommentFetcherService
    {
        private readonly IVoiceWallData data;

        public CommentFetcherService(IVoiceWallData data)
        {
            this.data = data;
        }

        public IQueryable<Comment> GetById(Guid id)
        {
            return this.data.Comments.All().Where(c => c.Id == id);
        }

        public CommentStateForUser CommentFlaggedByUser(Guid commentId, string userId)
        {
            return this.data.CommentViews.All().Where(cv => cv.UserId == userId && cv.CommentId == commentId)
                .Select(cv => new CommentStateForUser { IsFlagged = cv.Flagged }).FirstOrDefault();
        }
    }
}
