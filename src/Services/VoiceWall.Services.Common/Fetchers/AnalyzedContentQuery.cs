namespace VoiceWall.Services.Common.Fetchers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;

    public class AnalyzedContentQuery
    {
        public Content OriginalContent { get; set; }

        public ContentStateForUser ContentStateForUser { get; set; }

        public IEnumerable<CommentStateForUserCollection> ContentCommentsFlags { get; set; }
    }
}
