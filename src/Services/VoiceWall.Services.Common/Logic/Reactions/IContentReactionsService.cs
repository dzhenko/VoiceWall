namespace VoiceWall.Services.Common.Logic.Reactions
{
    using System;

    public interface IContentReactionsService
    {
        Guid FlagContent(Guid contentId, string userId);

        Guid LikeComment(Guid contentId, string userId);

        Guid HateComment(Guid contentId, string userId);
    }
}
