namespace VoiceWall.Services.Moderation
{
    using System;
    using System.Collections.Generic;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Moderation;

    public class ContentModerationService : BaseModerationService, IModerationService<Content>
    {
        public ContentModerationService(IVoiceWallData data)
            : base(data)
        {
        }

        public IEnumerable<Content> Read()
        {
            return this.Data.Contents.All();
        }

        public void ChangeHide(Guid id, bool isHidden)
        {
            var content = this.Data.Contents.GetById(id);

            if (content != null)
            {
                content.IsHidden = isHidden;
                this.Data.Contents.Update(content);
                this.Data.SaveChanges();
            }
        }
    }
}
