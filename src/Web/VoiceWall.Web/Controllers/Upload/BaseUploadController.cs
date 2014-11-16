namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using VoiceWall.Services.Common.Generators;
    using VoiceWall.Web.Infrastructure.Caching;

    /// <summary>
    /// Abstract controller used to provide access to content and comments Generators
    /// </summary>

    public abstract class BaseUploadController : BaseController
    {
        private readonly IUploadingGeneratorService uploadingGeneratorService;
        private readonly ICacheService cache;

        public BaseUploadController(IUploadingGeneratorService uploadingGeneratorService, ICacheService cache)
        {
            this.uploadingGeneratorService = uploadingGeneratorService;
            this.cache = cache;
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

            this.cache.Clear(contentId.ToString());

            return this.uploadingGeneratorService.Comment(file.InputStream,
                this.HttpContext.User.Identity.GetUserId(), file.ContentType, contentId);
        }
    }
}