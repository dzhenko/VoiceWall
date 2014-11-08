namespace VoiceWall.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class WallItemViewModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime Created { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Flags { get; set; }

        public bool? IsLiked { get; set; }

        public bool IsFlagged { get; set; }
    }
}