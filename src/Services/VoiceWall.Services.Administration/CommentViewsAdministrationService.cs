namespace VoiceWall.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;

    public class CommentViewsAdministrationService : BaseAdministrationService, IAdministrationService<CommentView>
    {
        public CommentViewsAdministrationService(IVoiceWallData data)
            : base(data)
        {
        }

        public void Create(CommentView entity)
        {
            this.Data.CommentViews.Add(entity);
            this.Data.SaveChanges();
        }

        public void Delete(object id)
        {
            this.Data.CommentViews.Delete(id);

            this.Data.SaveChanges();
        }

        public void Update(CommentView entity)
        {
            this.Data.CommentViews.Update(entity);
            this.Data.SaveChanges();
        }

        public IEnumerable<CommentView> Read()
        {
            return this.Data.CommentViews.All();
        }

        public CommentView Get(object id)
        {
            return this.Data.CommentViews.GetById(id);
        }
    }
}
