namespace VoiceWall.Data
{
    using System.Data.Entity;

    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

    public interface IVoiceWallData
    {
        IDeletableEntityRepository<User> Users { get; }

        IDeletableEntityRepository<Content> Contents { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<ContentView> ContentViews { get; }

        IDeletableEntityRepository<CommentView> CommentViews { get; }

        DbContext Context { get; }

        void SaveChanges();
    }
}
