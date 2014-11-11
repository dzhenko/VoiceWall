namespace VoiceWall.Web.Infrastructure.Filters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ValidateVideoFileAttribute : BaseValidateMediaFileAttribute
    {
        private readonly IList<string> allowedMimeTypes = new List<string>() { "video/webm", "video/mp4" };

        public override bool IsValid(object value)
        {
            try
            {
                this.ValidateOrThrowException(value, 1024 * 1024 * 25 /*25MB*/, allowedMimeTypes);
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
