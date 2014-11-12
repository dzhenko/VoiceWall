namespace VoiceWall.Services.Common.Fetchers
{
    using System;

    public class CommentStateForUserCollection
    {
        public Guid CommentId { get; set; }

        public bool IsFlagged { get; set; }
    }
}
