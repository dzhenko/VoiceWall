namespace VoiceWall.Services.Logic.Reactions
{
    using System;
    using System.Linq;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Logic.Reactions;

    public class CommentReactionsService : ICommentReactionsService
    {
        private readonly IVoiceWallData data;

        public CommentReactionsService(IVoiceWallData data)
        {
            this.data = data;
        }

        public Guid FlagComment(Guid commentId, string userId)
        {
            var view = this.data.CommentViews.All().FirstOrDefault(v => v.CommentId == commentId && v.UserId == userId);

            if (view == null)
            {
                var comment = this.data.Comments.All().FirstOrDefault(c => c.Id == commentId);

                if (comment == null)
                {
                    throw new ArgumentException("Comment does not exist");
                }

                this.data.CommentViews.Add(new CommentView()
                {
                    Flagged = true,
                    UserId = userId,
                    CommentId = commentId
                });
            }
            else
            {
                view.Flagged = !view.Flagged;
                this.data.CommentViews.Update(view);
            }

            this.data.CommentViews.SaveChanges();

            return commentId;
        }
    }
}
