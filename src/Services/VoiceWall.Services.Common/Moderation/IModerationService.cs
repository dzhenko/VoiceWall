namespace VoiceWall.Services.Common.Moderation
{
    using System;
    using System.Collections.Generic;

    using VoiceWall.Data.Common.Models;

    public interface IModerationService<T> where T : IDeletableEntity
    {
        IEnumerable<T> Read();

        void ChangeHide(Guid id, bool isHidden);
    }
}
