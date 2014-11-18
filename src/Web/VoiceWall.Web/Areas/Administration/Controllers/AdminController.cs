namespace VoiceWall.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using VoiceWall.Common;
    using VoiceWall.Web.Controllers;

    [Authorize(Roles=GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
    }
}