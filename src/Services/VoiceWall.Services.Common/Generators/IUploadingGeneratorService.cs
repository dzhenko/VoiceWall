namespace VoiceWall.Services.Common.Generators
{
    using System;
    using System.IO;

    public interface IUploadingGeneratorService
    {
        /// <summary>
        /// Creates a object, by uploading it.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <param name="ownerId">The owner of the file.</param>
        /// <param name="mimeType">The mime type of the file.</param>
        /// <returns>The id of the object.</returns>
        Guid Create(Stream file, string ownerId, string mimeType);

        /// <summary>
        /// Comments a object, by uploading it.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <param name="ownerId">The owner of the file.</param>
        /// <param name="mimeType">The mime type of the file.</param>
        /// <param name="contentId">The object id to put the comment on.</param>
        /// <returns>The id of the comment.</returns>
        Guid Comment(Stream file, string ownerId, string mimeType, Guid contentId);
    }
}