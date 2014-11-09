namespace VoiceWall.Web.ViewModels.Upload
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using VoiceWall.Web.Infrastructure.Filters;

    public class NewSoundCommentInputModel
    {
        [ValidateSoundFile]
        public HttpPostedFileBase File { get; set; }

        [Required]
        public Guid ContentId { get; set; }
    }
}