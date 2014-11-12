namespace VoiceWall.Web.Controllers.Upload
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Services.Common.Generators;

    /// <summary>
    /// Abstract controller used to provide access to content and comments Generators
    /// </summary>

    public abstract class BaseUploadController : BaseController
    {
        private readonly IUploadingGeneratorService uploadingGeneratorService;

        public BaseUploadController(IUploadingGeneratorService uploadingGeneratorService)
        {
            this.uploadingGeneratorService = uploadingGeneratorService;
        }

        public IUploadingGeneratorService UploadingGeneratorService
        {
            get { return this.uploadingGeneratorService; }
        }

        public Guid CreateContent(HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException(ModelState.Values.FirstOrDefault() == null ? "Invalid file" 
                    : ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }

            return this.uploadingGeneratorService.Create(file.InputStream, 
                this.HttpContext.User.Identity.GetUserId(), file.ContentType);
        }

        public Guid CommentContent(HttpPostedFileBase file, Guid contentId)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException(ModelState.Values.FirstOrDefault() == null ? "Invalid file"
                    : ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }

            return this.uploadingGeneratorService.Comment(file.InputStream,
                this.HttpContext.User.Identity.GetUserId(), file.ContentType, contentId);
        }
    }
}