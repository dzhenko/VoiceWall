namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using VoiceWall.Web.Infrastructure.Caching;

    public class BaseReactionsController : BaseController
    {
        private readonly ICacheService cache;

        public BaseReactionsController(ICacheService cache)
        {
            this.cache = cache;
        }

        protected ActionResult ConditionalCacheRemovingActionResult(Guid cacheItemId, Action action, ActionResult result)
        {
            this.cache.Clear(cacheItemId.ToString());
            return this.IndependentActionResult(() => action(), result);
        }
    }
}