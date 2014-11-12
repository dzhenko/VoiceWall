namespace VoiceWall.Services.Fetcher
{
    using System;
    using System.Linq;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;

    public class ContentFetcherService : IContentFetcherService
    {
        private readonly IVoiceWallData data;

        public ContentFetcherService(IVoiceWallData data)
        {
            this.data = data;
        }

        public IQueryable<Content> GetLast(int count = 5)
        {
            return this.data.Contents.All().OrderByDescending(c => c.CreatedOn).Take(count);
        }

        public IQueryable<Content> GetById(Guid id)
        {
            return this.data.Contents.All().Where(c => c.Id == id);
        }

        public ContentStateForUser ContentLikedFlaggedByUser(Guid contentId, string userId)
        {
            return this.data.ContentViews.All().Where(cv => cv.UserId == userId && cv.ContentId == contentId)
                .Select(cv => new ContentStateForUser { IsLiked = cv.Liked, IsFlagged = cv.Flagged }).FirstOrDefault();
        }
    }
}
