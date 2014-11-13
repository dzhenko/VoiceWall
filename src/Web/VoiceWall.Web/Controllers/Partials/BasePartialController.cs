namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public abstract class BasePartialController : BaseController
    {
        public ActionResult PartialActionResult(string identifier, Func<ActionResult> actionResult)
        {
            var cachedView = this.HttpContext.Cache[identifier] as ActionResult;

            if (cachedView == null)
            {
                cachedView = actionResult();
                this.HttpContext.Cache[identifier] = cachedView;
            }

            return cachedView;
        }
    }
}