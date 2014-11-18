namespace VoiceWall.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using VoiceWall.Data.Seeders;

    public sealed class Configuration : DbMigrationsConfiguration<VoiceWallDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VoiceWallDbContext context)
        {
            if (!context.Contents.Any())
            {
                StaticDataSeeder.SeedRoles(context);
                StaticDataSeeder.SeedAdmin(context);
                StaticDataSeeder.SeedModerator(context);
                StaticDataSeeder.SeedUsers(context);
                StaticDataSeeder.SeedData(context);
                StaticDataSeeder.SeedJokes(context);
            }
        }
    }
}
