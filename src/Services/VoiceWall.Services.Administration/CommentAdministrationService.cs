namespace VoiceWall.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;

    public class CommentAdministrationService : BaseAdministrationService, IAdministrationService<Comment>
    {
        public CommentAdministrationService(IVoiceWallData data)
            : base(data)
        {
        }

        public void Create(Comment entity)
        {
            this.Data.Comments.Add(entity);
            this.Data.SaveChanges();
        }

        public void Delete(object id)
        {
            var comment = this.Data.Comments.GetById(id);

            foreach (var commentViewId in comment.CommentViews.Select(c => c.Id).ToList())
            {
                this.Data.CommentViews.Delete(commentViewId);
            }

            this.Data.SaveChanges();

            this.Data.Comments.Delete(comment);

            this.Data.SaveChanges();
        }

        public void Update(Comment entity)
        {
            this.Data.Comments.Update(entity);
            this.Data.SaveChanges();
        }

        public IEnumerable<Comment> Read()
        {
            return this.Data.Comments.All();
        }

        public Comment Get(object id)
        {
            return this.Data.Comments.GetById(id);
        }
    }
}
