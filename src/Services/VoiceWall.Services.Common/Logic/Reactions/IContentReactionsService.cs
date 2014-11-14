namespace VoiceWall.Services.Common.Logic.Reactions
{
    using System;

    public interface IContentReactionsService
    {
        /// <summary>
        /// Flags a content.
        /// </summary>
        /// <param name="commentId">The content to flag.</param>
        /// <param name="userId">The id of the user that is flagging the content.</param>
        /// <returns>The content id.</returns>
        Guid FlagContent(Guid contentId, string userId);

        /// <summary>
        /// Likes a content.
        /// </summary>
        /// <param name="commentId">The content to like.</param>
        /// <param name="userId">The id of the user that is liking the content.</param>
        /// <returns>The content id.</returns>
        Guid LikeComment(Guid contentId, string userId);

        /// <summary>
        /// Hates a content.
        /// </summary>
        /// <param name="commentId">The content to hate.</param>
        /// <param name="userId">The id of the user that is hating the content.</param>
        /// <returns>The content id.</returns>
        Guid HateComment(Guid contentId, string userId);
    }
}
