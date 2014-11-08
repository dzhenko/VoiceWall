namespace VoiceWall.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VoiceWall.Data.Common.Models;

    public class ContentView : DeletableEntity
    {
        public ContentView()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid ContentId { get; set; }

        public virtual Content Content { get; set; }

        public bool? Liked { get; set; }

        public bool Flagged { get; set; }
    }
}
