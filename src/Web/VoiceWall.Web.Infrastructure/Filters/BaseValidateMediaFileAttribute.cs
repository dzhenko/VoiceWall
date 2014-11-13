namespace VoiceWall.Web.Infrastructure.Filters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public abstract class BaseValidateMediaFileAttribute : ValidationAttribute
    {
        protected void ValidateOrThrowException(object value, int allowedMaxSize, IList<string> allowedMimeTypes)
        {
            var fileAsHttpPostedFileBase = value as HttpPostedFileBase;

            if (fileAsHttpPostedFileBase == null)
            {
                throw new ArgumentException("Please upload a file");
            }

            if (fileAsHttpPostedFileBase.ContentLength == 0)
            {
                throw new ArgumentException("Please upload a non-empty file");
            }

            if (fileAsHttpPostedFileBase.ContentLength > allowedMaxSize)
            {
                throw new ArgumentException(string.Format("File size can not exceed {0}", allowedMaxSize));
            }

            if (!allowedMimeTypes.Contains(fileAsHttpPostedFileBase.ContentType))
            {
                throw new ArgumentException("File type not supported");
            }
        }
    }
}
