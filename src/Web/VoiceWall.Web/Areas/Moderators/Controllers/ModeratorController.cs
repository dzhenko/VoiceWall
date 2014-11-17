namespace VoiceWall.Web.Areas.Moderators.Controllers
{
    using System.Web.Mvc;

    using VoiceWall.Common;
    using VoiceWall.Web.Controllers;
    using VoiceWall.Web.Infrastructure.Caching;

    [Authorize(Roles=GlobalConstants.ModeratorRole)]
    public abstract class ModeratorController : BaseController
    {
        private readonly ICacheService cache;

        public ModeratorController(ICacheService cache)
        {
            this.cache = cache;
        }

        protected ICacheService Cache
        {
            get { return this.cache; }
        }
    }
}