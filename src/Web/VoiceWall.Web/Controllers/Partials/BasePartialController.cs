namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using VoiceWall.Web.Infrastructure.Caching;

    public abstract class BasePartialController : BaseController // in memory cache
    {
        private readonly ICacheService cache;

        public BasePartialController(ICacheService cache)
        {
            this.cache = cache;
        }

        public ActionResult PartialActionResult(string identifier, Func<ActionResult> actionResult)
        {
            return this.cache.Get<ActionResult>(identifier, actionResult);
        }
    }
}