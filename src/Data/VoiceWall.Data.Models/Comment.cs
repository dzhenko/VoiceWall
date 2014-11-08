namespace VoiceWall.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using VoiceWall.Data.Common.Models;

    public class Comment : DeletableEntity
    {
        private ICollection<CommentView> commentViews;

        public Comment()
        {
            this.Id = Guid.NewGuid();
            this.commentViews = new HashSet<CommentView>();
        }

        public Guid Id { get; set; }

        [Required]
        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public Guid ContentId { get; set; }

        public virtual Content Content { get; set; }

        public virtual ICollection<CommentView> CommentViews
        {
            get { return this.commentViews; }
            set { this.commentViews = value; }
        }
    }
}
