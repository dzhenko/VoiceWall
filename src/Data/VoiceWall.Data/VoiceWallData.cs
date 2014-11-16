namespace VoiceWall.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VoiceWall.Data.Common.Models;
    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;

    public class VoiceWallData : IVoiceWallData
    {
        private IVoiceWallDbContext context;
        private IDictionary<Type, object> repositories;

        public static IVoiceWallData Create(IVoiceWallDbContext context)
        {
            return new VoiceWallData(context);
        }

        public VoiceWallData(IVoiceWallDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IVoiceWallDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IDeletableEntityRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IDeletableEntityRepository<Content> Contents
        {
            get
            {
                return this.GetRepository<Content>();
            }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IDeletableEntityRepository<ContentView> ContentViews
        {
            get
            {
                return this.GetRepository<ContentView>();
            }
        }

        public IDeletableEntityRepository<CommentView> CommentViews
        {
            get
            {
                return this.GetRepository<CommentView>();
            }
        }
        
        public void SaveChanges()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfModel];
        }
    }
}
