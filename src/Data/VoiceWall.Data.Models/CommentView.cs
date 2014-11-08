namespace VoiceWall.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VoiceWall.Data.Common.Models;

    public class CommentView : DeletableEntity
    {
        public CommentView()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public bool Flagged { get; set; }
    }
}
