namespace VoiceWall.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class WallItemViewModel
    {
        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string ContentUrl { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime Created { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Flags { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}