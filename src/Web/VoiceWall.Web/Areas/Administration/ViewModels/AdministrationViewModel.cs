namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public abstract class AdministrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public object Id { get; set; }

        [Display(Name = "Added on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }
    }
}