namespace VoiceWall.Web
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

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

        public static MvcHtmlString ActionLink<TCtrl>(this HtmlHelper helper, Expression<Action<TCtrl>> expression,
            string linkText, object rootValues = null, object htmlAttributes = null) where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");

            var action = ((MethodCallExpression)expression.Body).Method.Name;

            if (rootValues == null)
            {
                rootValues = new { area = string.Empty };
            }

            return helper.ActionLink(linkText, action, ctrl, rootValues, htmlAttributes);
        }

        public static MvcForm BeginForm<TCtrl>(this HtmlHelper helper, Expression<Action<TCtrl>> expression,
            object rootValues = null, FormMethod method = FormMethod.Post, object htmlAttributes = null) where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");

            var action = ((MethodCallExpression)expression.Body).Method.Name;

            return helper.BeginForm(action, ctrl, rootValues, method, htmlAttributes);
        }

        public static MvcHtmlString Action<TCtrl>(this HtmlHelper helper, Expression<Action<TCtrl>> expression,
            object rootValues = null) where TCtrl : Controller
        {
            var ctrl = typeof(TCtrl).Name.Replace("Controller", "");

            var action = ((MethodCallExpression)expression.Body).Method.Name;

            return helper.Action(action, ctrl, rootValues);
        }
    }
}