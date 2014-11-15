namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq.Expressions;
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

        protected ActionResult RedirectToAction<TCtrl>(Expression<Action<TCtrl>> expression) where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");
            
            var action = ((MethodCallExpression)expression.Body).Method.Name;

            return this.RedirectToAction(action, ctrl);
        }

        protected ActionResult RedirectToActionPermanent<TCtrl>(Expression<Action<TCtrl>> expression) where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");

            var action = ((MethodCallExpression)expression.Body).Method.Name;

            return this.RedirectToActionPermanent(action, ctrl);
        }
    }
}