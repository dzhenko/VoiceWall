namespace VoiceWall.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using VoiceWall.Data.Models;

    public interface IVoiceWallDbContext
    {
        IDbSet<T> Set<T>() where T : class;

        IDbSet<Content> Contents { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<ContentView> ContentViews { get; set; }

        IDbSet<CommentView> CommentViews { get; set; }

        IDbSet<User> Users { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
