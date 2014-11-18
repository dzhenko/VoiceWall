namespace VoiceWall.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using VoiceWall.Data.Migrations;
    using VoiceWall.Data.Models;
    using VoiceWall.Data.Common.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class VoiceWallDbContext : IdentityDbContext<User>, IVoiceWallDbContext
    {
        public VoiceWallDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoiceWallDbContext, Configuration>());
        }

        public static VoiceWallDbContext Create()
        {
            return new VoiceWallDbContext();
        }

        public IDbSet<Content> Contents { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<ContentView> ContentViews { get; set; }

        public IDbSet<CommentView> CommentViews { get; set; }

        public IDbSet<Joke> Jokes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        // for the future :)
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }
    }
}
