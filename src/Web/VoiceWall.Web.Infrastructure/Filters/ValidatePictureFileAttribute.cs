namespace VoiceWall.Web.Infrastructure.Filters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ValidatePictureFileAttribute : BaseValidateMediaFileAttribute
    {
        private readonly IList<string> allowedMimeTypes = new List<string>() { "image/jpeg", "image/png" };

        public override bool IsValid(object value)
        {
            try
            {
                this.ValidateOrThrowException(value, 1024 * 1024 * 5 /*5MB*/, allowedMimeTypes);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }
    }
}
