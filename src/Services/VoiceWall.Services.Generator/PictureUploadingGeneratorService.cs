namespace VoiceWall.Services.Generator
{
    using System;
    using System.IO;
    using System.Linq;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Generators;

    public class PictureUploadingGeneratorService : BaseUploadingGeneratorService, IPictureUploadingGeneratorService
    {
        public PictureUploadingGeneratorService(IVoiceWallData data, IPicturesCloudStorage videoStorage)
            : base (data, videoStorage)
        {
        }

        public Guid Create(Stream file, string ownerId, string mimeType)
        {
            return this.CreateContent(file, ContentType.Picture, ownerId, mimeType);
        }

        public Guid Comment(Stream file, string ownerId, string mimeType, Guid contentId)
        {
            return this.CreateComment(file, ContentType.Picture, ownerId, mimeType, contentId);
        }
    }
}
