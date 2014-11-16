namespace VoiceWall.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;

    public class ContentAdministrationService : BaseAdministrationService, IAdministrationService<Content>
    {
        public ContentAdministrationService(IVoiceWallData data)
            : base(data)
        {
        }

        public void Create(Content entity)
        {
            this.Data.Contents.Add(entity);
            this.Data.SaveChanges();
        }

        public void Delete(object id)
        {
            var content = this.Data.Contents.GetById(id);

            foreach (var commentId in content.Comments.Select(c => c.Id).ToList())
            {
                var commentViews = this.Data.CommentViews
                    .All()
                    .Where(c => c.Id == commentId)
                    .Select(com => com.Id)
                    .ToList();

                foreach (var commentViewId in commentViews)
                {
                    this.Data.CommentViews.Delete(commentViewId);
                }

                this.Data.SaveChanges();

                this.Data.Comments.Delete(commentId);
            }

            this.Data.SaveChanges();

            foreach (var contentViewId in content.ContentViews.Select(c => c.Id).ToList())
            {
                this.Data.ContentViews.Delete(contentViewId);
            }

            this.Data.SaveChanges();

            this.Data.Contents.Delete(content);

            this.Data.SaveChanges();
        }

        public void Update(Content entity)
        {
            this.Data.Contents.Update(entity);
            this.Data.SaveChanges();
        }

        public IEnumerable<Content> Read()
        {
            return this.Data.Contents.All();
        }

        public Content Get(object id)
        {
            return this.Data.Contents.GetById(id);
        }
    }
}
