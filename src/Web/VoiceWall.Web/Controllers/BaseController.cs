namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using VoiceWall.Data;

    /// <summary>
    /// Abstract controller used to provide uow to its successors.
    /// </summary>

    public abstract class BaseController : Controller
    {
        protected ActionResult ConditionalActionResult<T>(Func<T> funcToPerform, Func<T, ActionResult> resultToReturn)
        {
            try
            {
                var result = funcToPerform();
                return resultToReturn(result);
            }
            catch (Exception ex)
            {
                return this.HttpNotFound(ex.Message);
            }
        }

        protected ActionResult IndependentActionResult(Action actionToPerform, ActionResult resultToReturn)
        {
            try
            {
                actionToPerform();
                return resultToReturn;
            }
            catch (Exception ex)
            {
                return this.HttpNotFound(ex.Message);
            }
        }
    }
}