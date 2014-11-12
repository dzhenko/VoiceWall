namespace VoiceWall.Services.Generator
{
    using System;
    using System.IO;
    using System.Linq;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;
    using VoiceWall.Data.Models;

    public abstract class BaseUploadingGeneratorService : BaseGeneratorService
    {
        private readonly ICloudStorage storage;

        public BaseUploadingGeneratorService(IVoiceWallData data, ICloudStorage storage)
            : base(data)
        {
            this.storage = storage;
        }

        protected ICloudStorage Storage
        {
            get { return this.storage; }
        }

        public Guid CreateContent(Stream file, ContentType type, string ownerId, string mimeType)
        {
            var content = new Content()
            {
                ContentType = type,
                UserId = ownerId
            };

            content.ContentUrl = this.Storage.UploadFile(file, content.Id.ToString(), mimeType);

            this.Data.Contents.Add(content);
            this.Data.SaveChanges();

            return content.Id;
        }

        public Guid CreateComment(Stream file, ContentType type, string ownerId, string mimeType, Guid contentId)
        {
            if (!this.Data.Contents.All().Any(c => c.Id == contentId))
            {
                throw new ArgumentException("No content with this id");
            }

            var comment = new Comment()
            {
                ContentType = type,
                UserId = ownerId,
                ContentId = contentId
            };

            comment.ContentUrl = this.Storage.UploadFile(file, comment.Id.ToString(), mimeType);

            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();

            return comment.Id;
        }
    }
}
