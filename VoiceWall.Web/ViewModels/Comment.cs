namespace VoiceWall.Web.ViewModels
{
    using System;

    public class Comment
    {
        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime Created { get; set; }
    }
}
