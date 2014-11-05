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

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<View> Views { get; set; }
    }
}