namespace VoiceWall.Services.Common.Fetchers
{
    using System;
    using System.Linq;

    using VoiceWall.Data.Models;

    public interface IContentFetcherService
    {
        IQueryable<Content> GetLast(int count = 5);

        IQueryable<AnalyzedContentQuery> GetLastWithStats(string userId, int count = 5);

        IQueryable<Content> GetById(Guid id);

        ContentStateForUser ContentLikedFlaggedByUser(Guid contentId, string userId);

        IQueryable<Content> GetNext(int skip, int count);
    }
}
