namespace VoiceWall.Services.Generator
{
    using System;
    using System.IO;
    using System.Linq;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Generators;

    public class SoundUploadingGeneratorService : BaseUploadingGeneratorService, ISoundUploadingGeneratorService
    {
        public SoundUploadingGeneratorService(IVoiceWallData data, ISoundsCloudStorage videoStorage)
            : base (data, videoStorage)
        {
        }

        public Guid Create(Stream file, string ownerId, string mimeType)
        {
            return this.CreateContent(file, ContentType.Sound, ownerId, mimeType);
        }

        public Guid Comment(Stream file, string ownerId, string mimeType, Guid contentId)
        {
            return this.CreateComment(file, ContentType.Sound, ownerId, mimeType, contentId);
        }
    }
}
