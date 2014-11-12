namespace VoiceWall.Services.Common.Logic.Reactions
{
    using System;

    public interface ICommentReactionsService
    {
        Guid FlagComment(Guid commentId, string userId);
    }
}
