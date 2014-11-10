namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using VoiceWall.Data.Common.Repositories;
    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Filters;

    /// <summary>
    /// Used as an endpoint for ajax requests for reactions and returns partials of the updated content.
    /// </summary>

    [Authorize]
    public class ReactionsController : BaseContentAndCommentController
    {
        public ReactionsController(IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
            : base(contentsRepository, commentsRepository)
        {
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(Guid contentId)
        {
            return null;
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Hate(Guid contentId)
        {
            return null;
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult Flag(Guid contentId)
        {
            return null;
        }

        [AjaxPost]
        [ValidateAntiForgeryToken]
        public ActionResult FlagComment(Guid contentId)
        {
            System.Threading.Thread.Sleep(2000);
            return Content(@"<div class=""row single-comment-item"" data-wall-item-comment-id=""de80ce68-e6f9-4385-991e-09778cf2b1fe"" style=""display: block;""><div class=""col-md-3""><div class=""comment-image-holder"" style=""background-image:url(http://2.bp.blogspot.com/-dZKdgsUW2y0/Une2h3IIVMI/AAAAAAAAC1o/tqJJFHKzHfY/s1600/katrina-kaif-Complete-Profile.jpg)""></div></div><div class=""col-md-4""><div class=""row""><a href=""/Home/Details?UserID=08e76caf-60b7-4ff4-b477-ec6316f61928"">Vicktoria Petrova</a></div><div class=""row"">00:11 09/11/2014</div><div class=""row""><i class=""fa fa-2x fa-fw fa-flag-o text-danger"">1</i></div></div><div class=""col-md-1""><button class=""btn btn-dark flagCommentBtn""><i class=""fa fa-fw fa-flag fa-2x""></i><br><span>Flag</span></button></div><div class=""col-md-3 col-md-offset-1""><a class=""small-multimedia-main-action"" data-content-src=""http://video.webmfiles.org/big-buck-bunny_trailer.webm"" data-content-type=""Video"" href=""""><i class=""fa-play-circle fa fa-fw""></i></a></div></div>");
        }

        private ActionResult GetWallItemHolder(Guid wallItemId)
        {
            return null;
        }
    }
}