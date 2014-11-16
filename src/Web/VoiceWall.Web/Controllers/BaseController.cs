namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    /// <summary>
    /// Abstract controller used to provide uow to its successors.
    /// </summary>

    public abstract class BaseController : Controller
    {
        protected ActionResult ConditionalActionResult<T>(Func<T> funcToPerform, Func<T, ActionResult> resultToReturn)
        {
            if (ModelState.IsValid)
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

            else
            {
                return this.HttpNotFound(ModelState.Values.FirstOrDefault().ToString());
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

        protected ActionResult RedirectToAction<TCtrl>(Expression<Action<TCtrl>> expression, object routeValues = null)
            where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");

            var action = ((MethodCallExpression)expression.Body).Method.Name;

            return this.RedirectToAction(action, ctrl, routeValues);
        }

        protected ActionResult RedirectToActionPermanent<TCtrl>(Expression<Action<TCtrl>> expression, object routeValues = null)
            where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");

            var action = ((MethodCallExpression)expression.Body).Method.Name;

            return this.RedirectToActionPermanent(action, ctrl, routeValues);
        }
    }
}