namespace VoiceWall.Services.Common.Fetchers
{
    using System;
    using System.Linq;

    using VoiceWall.Data.Models;

    public interface ICommentFetcherService
    {
        IQueryable<Comment> GetById(Guid id);

        CommentStateForUser CommentFlaggedByUser(Guid commentId, string userId);
    }
}
