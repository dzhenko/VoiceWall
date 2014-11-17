namespace VoiceWall.Services.Moderation
{
    using System;
    using System.Collections.Generic;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Moderation;

    public class CommentModerationService : BaseModerationService, IModerationService<Comment>
    {
        public CommentModerationService(IVoiceWallData data)
            : base(data)
        {
        }

        public IEnumerable<Comment> Read()
        {
            return this.Data.Comments.All();
        }

        public void ChangeHide(Guid id, bool isHidden)
        {
            var comment = this.Data.Comments.GetById(id);

            if (comment != null)
            {
                comment.IsHidden = isHidden;
                this.Data.Comments.Update(comment);
                this.Data.SaveChanges();
            }
        }
    }
}
