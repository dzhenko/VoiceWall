namespace VoiceWall.Web.ViewModels
{
    using System;

    public class Comment
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public int Flags { get; set; }

        public bool IsFlagged { get; set; }

        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime Created { get; set; }
    }
}
