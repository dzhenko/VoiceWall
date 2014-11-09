namespace VoiceWall.Web.Infrastructure.Filters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ValidateVideoFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // 25MB
            const int AllowedMaxSize = 1024 * 1024 * 25;

            var fileAsHttpPostedFileBase = value as HttpPostedFileBase;

            if (fileAsHttpPostedFileBase == null)
            {
                ErrorMessage = "Please upload a file";
                return false;
            }

            if (fileAsHttpPostedFileBase.ContentLength > AllowedMaxSize)
            {
                ErrorMessage = string.Format("File size can not exceed {0}", AllowedMaxSize);
                return false;
            }

            var allowedMimeTypes = new List<string>() { "video/webm", "video/mp4" };

            if (!allowedMimeTypes.Contains(fileAsHttpPostedFileBase.ContentType))
            {
                ErrorMessage = "File type not supported";
                return false;
            }

            return true;
        }
    }
}
