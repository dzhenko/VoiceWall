namespace VoiceWall.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using VoiceWall.Common;
    using VoiceWall.Web.Controllers;
    using VoiceWall.Web.Infrastructure.Caching;

    [Authorize(Roles=GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        private readonly ICacheService cache;

        public AdminController(ICacheService cache)
        {
            this.cache = cache;
        }

        protected ICacheService Cache
        {
            get { return this.cache; }
        }
    }
}