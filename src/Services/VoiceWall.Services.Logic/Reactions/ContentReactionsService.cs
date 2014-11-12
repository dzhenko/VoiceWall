namespace VoiceWall.Services.Logic.Reactions
{
    using System;
    using System.Linq;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Logic.Reactions;

    public class ContentReactionsService : IContentReactionsService
    {
        private readonly IVoiceWallData data;

        public ContentReactionsService(IVoiceWallData data)
        {
            this.data = data;
        }

        public Guid FlagContent(Guid contentId, string userId)
        {
            return this.AlterViewLikeOrFlag(contentId, userId, null, true);
        }

        public Guid LikeComment(Guid contentId, string userId) 
        {
            return this.AlterViewLikeOrFlag(contentId, userId, true);
        }

        public Guid HateComment(Guid contentId, string userId)
        {
            return this.AlterViewLikeOrFlag(contentId, userId, false);
        }

        private Guid AlterViewLikeOrFlag(Guid contentId, string userId, bool? like, bool? flag = null)
        {
            var view = this.data.ContentViews.All().FirstOrDefault(v => v.ContentId == contentId && v.UserId == userId);

            if (view == null)
            {
                var content = this.data.Contents.All().FirstOrDefault(c => c.Id == contentId);

                if (content == null)
                {
                    throw new ArgumentException("Content does not exist");
                }

                var contentView = new ContentView() { UserId = userId };

                if (like.HasValue)
                {
                    contentView.Liked = like.Value;
                }

                if (flag.HasValue)
                {
                    contentView.Flagged = flag.Value;
                }
            }
            else
            {
                if (like.HasValue)
                {
                    // if they have the same value they are nulled
                    if (like.Value == view.Liked)
                    {
                        view.Liked = null;
                    }
                    else
                    {
                        view.Liked = like.Value;
                    }
                }

                else if (flag.HasValue)
                {
                    view.Flagged = !view.Flagged;
                }
            }

            this.data.ContentViews.Update(view);
            this.data.ContentViews.SaveChanges();

            return contentId;
        }
    }
}
