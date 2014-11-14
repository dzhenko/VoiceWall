namespace VoiceWall.Services.Common.Logic.Reactions
{
    using System;

    public interface ICommentReactionsService
    {
        /// <summary>
        /// Flags a comment.
        /// </summary>
        /// <param name="commentId">The comment to flag.</param>
        /// <param name="userId">The id of the user that is flagging the comment.</param>
        /// <returns>The comment id.</returns>
        Guid FlagComment(Guid commentId, string userId);
    }
}
