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
            return this.data.Contents.All().Where(c => !c.IsHidden).OrderByDescending(c => c.CreatedOn).Take(count);
        }

        public IQueryable<Content> GetNext(int skip = 5, int count = 5)
        {
            return this.data.Contents.All().Where(c => !c.IsHidden).OrderByDescending(c => c.CreatedOn).Skip(skip).Take(count);
        }

        public IQueryable<AnalyzedContentQuery> GetLastWithStats(string userId, int count = 5)
        {
            var analyzedContents = this.data.Contents.All()
                .Where(c => !c.IsHidden)
                .OrderByDescending(c => c.CreatedOn)
                .Take(count)
                .Select(c => new AnalyzedContentQuery()
                {
                    OriginalContent = c,
                    ContentStateForUser = c.ContentViews.Where(cv => cv.UserId == userId).Select(cv => new ContentStateForUser()
                    {
                        IsLiked = cv.Liked,
                        IsFlagged = cv.Flagged
                    }).FirstOrDefault(),
                    ContentCommentsFlags = c.Comments
                        .Select(cm => cm.CommentViews.Where(cmv => cmv.UserId == userId)
                            .Select(cmv => new CommentStateForUserCollection
                            {
                                CommentId = cmv.Id,
                                IsFlagged = cmv.Flagged
                            })
                            .FirstOrDefault())
                });

            return analyzedContents;
        }

        public IQueryable<Content> GetById(Guid id)
        {
            return this.data.Contents.All().Where(c => c.Id == id && !c.IsHidden);
        }

        public ContentStateForUser ContentLikedFlaggedByUser(Guid contentId, string userId)
        {
            return this.data.ContentViews.All().Where(cv => cv.UserId == userId && cv.ContentId == contentId)
                .Select(cv => new ContentStateForUser { IsLiked = cv.Liked, IsFlagged = cv.Flagged }).FirstOrDefault();
        }
    }
}
