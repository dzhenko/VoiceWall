namespace VoiceWall.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;

    public class ContentViewsAdministrationService  : BaseAdministrationService, IAdministrationService<ContentView>
    {
        public ContentViewsAdministrationService(IVoiceWallData data)
            : base(data)
        {
        }

        public void Create(ContentView entity)
        {
            this.Data.ContentViews.Add(entity);
            this.Data.SaveChanges();
        }

        public void Delete(object id)
        {
            this.Data.ContentViews.Delete(id);

            this.Data.SaveChanges();
        }

        public void Update(ContentView entity)
        {
            this.Data.ContentViews.Update(entity);
            this.Data.SaveChanges();
        }

        public IEnumerable<ContentView> Read()
        {
            return this.Data.ContentViews.All();
        }

        public ContentView Get(object id)
        {
            return this.Data.ContentViews.GetById(id);
        }
    }
}
