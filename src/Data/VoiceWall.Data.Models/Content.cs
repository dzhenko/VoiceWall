namespace VoiceWall.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VoiceWall.Data.Common.Models;

    public class Content : DeletableEntity
    {
        private ICollection<Comment> comments;
        private ICollection<ContentView> contentViews;

        public Content()
        {
            this.Id = Guid.NewGuid();
            this.comments = new HashSet<Comment>();
            this.contentViews = new HashSet<ContentView>();
        }

        public Guid Id { get; set; }

        [Required]
        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<ContentView> ContentViews
        {
            get { return this.contentViews; }
            set { this.contentViews = value; }
        }
    }
}
