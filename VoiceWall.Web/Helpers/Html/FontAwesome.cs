namespace VoiceWall.Web.Helpers.Html
{
    using System.Text;
    using System.Web.Mvc;

    using VoiceWall.Web.ViewModels;

    public static class FontAwesome
    {
        //      <button class="commentBtn btn btn-dark pull-left">
        //          <i class="fa fa-fw fa-3x fa-comment"></i>
        //          <br/>
        //          <span>Comment</span>
        //      </button>
        public static MvcHtmlString FontAwesomeButton(this HtmlHelper helper, string value, string type, object htmlAttributes = null, bool clicked = false, int size = 3)
        {
            var button = new TagBuilder("button");
            button.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var buttonValue = new TagBuilder("span");
            buttonValue.InnerHtml = value;

            button.AddCssClass("btn btn-dark");

            if (clicked)
            {
                buttonValue.InnerHtml = "Un-" + buttonValue.InnerHtml;
            }

            var fontAwesome = new TagBuilder("i");
            fontAwesome.AddCssClass(string.Format("fa fa-fw fa-{0} fa-{1}x", type, size));

            button.InnerHtml = string.Format("{0}{1}{2}", fontAwesome.ToString(), "<br/>", buttonValue.ToString());

            return new MvcHtmlString(button.ToString());
        }

        //<a href="#" class="image-main-action" data-content-src="@Model.ContentUrl" data-content-type="@Model.ContentType">
        //    <i class="fa fa-fw fa-picture-o"></i>
        //</a>
        public static MvcHtmlString FontAwesomeMultimediaActionLink(this HtmlHelper helper, string contentUrl, ContentType type, object htmlAttributes = null)
        {
            var icon = new TagBuilder("i");
            icon.AddCssClass("fa fa-fw");
            switch (type)
            {
                case ContentType.Picture:
                    icon.AddCssClass("fa-picture-o");
                    break;
                case ContentType.Sound:
                    icon.AddCssClass("fa-volume-up");
                    break;
                case ContentType.Video:
                    icon.AddCssClass("fa-play-circle");
                    break;
            }

            var link = new TagBuilder("a");
            link.Attributes.Add("href", string.Empty);
            link.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            link.Attributes.Add("data-content-src", contentUrl);
            link.Attributes.Add("data-content-type", type.ToString());

            link.InnerHtml = icon.ToString();

            return new MvcHtmlString(link.ToString());
        }

        //<i class="fa fa-2x fa-fw fa-thumbs-o-up text-success">123</i>
        //<i class="fa fa-2x fa-fw fa-thumbs-o-down text-danger">222</i>
        //<i class="fa fa-2x fa-fw fa-eye text-primary">1242</i>
        //<i class="fa fa-2x fa-fw fa-flag-o text-danger">4</i>
        public static MvcHtmlString FontAwesomeItemStats(this HtmlHelper helper, int likes, int hates, int views, int flags = 0, object htmlAttributes = null)
        {
            var sb = new StringBuilder();

            var likesTag = new TagBuilder("i");
            likesTag.AddCssClass("fa fa-2x fa-fw fa-thumbs-o-up text-success");
            likesTag.InnerHtml = likes.ToString();
            sb.Append(likesTag.ToString());

            var hatesTag = new TagBuilder("i");
            hatesTag.AddCssClass("fa fa-2x fa-fw fa-thumbs-o-down text-danger");
            hatesTag.InnerHtml = hates.ToString();
            sb.Append(hatesTag.ToString());

            var viewsTag = new TagBuilder("i");
            viewsTag.AddCssClass("fa fa-2x fa-fw fa-eye text-primary");
            viewsTag.InnerHtml = views.ToString();
            sb.Append(viewsTag.ToString());

            if (flags > 0)
            {
                var flagsTag = new TagBuilder("i");
                flagsTag.AddCssClass("fa fa-2x fa-fw fa-flag-o text-danger");
                flagsTag.InnerHtml = flags.ToString();
                sb.Append(flagsTag.ToString());
            }

            return new MvcHtmlString(sb.ToString());
        }
    }
}