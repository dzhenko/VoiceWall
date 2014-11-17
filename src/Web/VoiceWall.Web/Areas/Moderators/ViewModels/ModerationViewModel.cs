namespace VoiceWall.Web.Areas.Moderators.ViewModels
{
    using System;
    using System.Web.Mvc;

    public abstract class ModerationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public bool IsHidden { get; set; }
    }
}