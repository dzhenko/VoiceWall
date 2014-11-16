namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public abstract class AdministrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
    }
}