namespace VoiceWall.Services.Common.Users
{
    using System.IO;
    using System.Linq;

    using VoiceWall.Data.Models;

    public interface IUserProfileService
    {
        /// <summary>
        /// Gets the user model by id.
        /// </summary>
        /// <param name="userId">The id of the user to get.</param>
        /// <returns>User as queryable.</returns>
        IQueryable<User> GetUserProfile(string userId);

        /// <summary>
        /// Updates the profile of the user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="model">The new profile.</param>
        /// <returns>The Id of the user.</returns>
        string UpdateUserProfile(string userId, UserUpdateProfileModel model);

        /// <summary>
        /// Changes the profile picture of the user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="stream">Picture file stream.</param>
        /// <param name="mimeType">Picture file mimetype.</param>
        /// <returns>The Id of the user.</returns>
        string ChangeUserPicture(string userId, Stream stream, string mimeType);

        /// <summary>
        /// Gets next wall item contents for a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="skip">The number of contents to skip.</param>
        /// <param name="count">The number of contents to take.</param>
        /// <returns>The extracted contents.</returns>
        IQueryable<Content> GetNext(string userId, int skip = 5, int count = 5);
    }
}
