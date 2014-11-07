namespace VoiceWall.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using VoiceWall.Data.Migrations;
    using VoiceWall.Data.Models;

    public class VoiceWallDbContext : IdentityDbContext<User>
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
    }
}
