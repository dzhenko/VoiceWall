namespace VoiceWall.Services.Common.Generators
{
    using System;
    using System.IO;

    public interface IUploadingGeneratorService
    {
        Guid Create(Stream file, string ownerId, string mimeType);

        Guid Comment(Stream file, string ownerId, string mimeType, Guid contentId);
    }
}