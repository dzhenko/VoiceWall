namespace VoiceWall.Web.Helpers.Html
{
    using System;
using System.Web.Mvc;
using VoiceWall.Web.ViewModels;

    public static class Standart
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, string value, object htmlAttributes = null)
        {
            var submitButton = new TagBuilder("input");
            submitButton.AddCssClass("btn btn-default");
            submitButton.Attributes.Add("type", "submit");
            submitButton.Attributes.Add("value", value);
            submitButton.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new MvcHtmlString(submitButton.ToString());
        }

        public static MvcHtmlString DivWithImage(this HtmlHelper helper, string imageUrl, object htmlAttributes = null)
        {
            var div = new TagBuilder("div");
            div.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            div.Attributes.Add("style", string.Format("background-image:url({0})", imageUrl));
            return new MvcHtmlString(div.ToString());
        }
    }
}