namespace VoiceWall.Web.ViewModels
{
    using System.Collections.Generic;

    public class WallItemWithCommentsViewModel
    {
        public WallItemViewModel WallItemViewModel { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}