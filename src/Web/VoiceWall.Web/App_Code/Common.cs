namespace VoiceWall.Web
{
    using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using VoiceWall.Web.ViewModels;

    public static class Common
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, string value, object htmlAttributes = null)
        {
            var submitButton = new TagBuilder("input");
            submitButton.Attributes.Add("type", "submit");
            submitButton.Attributes.Add("value", value);
            submitButton.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            submitButton.AddCssClass("btn btn-dark");
            return new MvcHtmlString(submitButton.ToString());
        }

        public static MvcHtmlString DivWithImage(this HtmlHelper helper, string imageUrl, object htmlAttributes = null)
        {
            var div = new TagBuilder("div");
            div.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            div.Attributes.Add("style", string.Format("background-image:url({0})", imageUrl));
            return new MvcHtmlString(div.ToString());
        }

        //public static MvcHtmlString ActionLink<TController>(this HtmlHelper helper, Expression<Action<TController>> action,
        //    object rootValues = null, object htmlAttributes = null) where TController : Controller
        //{
        //    var controllerName = typeof(TController).Name;

        //    var actionName = action.Compile().Method.Name;

        //    return null;
        //}
    }
}