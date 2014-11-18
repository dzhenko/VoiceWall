namespace VoiceWall.Web.Areas.Moderators.Controllers
{
    using System.Web.Mvc;

    using VoiceWall.Common;
    using VoiceWall.Web.Controllers;
    using VoiceWall.Web.Infrastructure.Caching;

    [Authorize(Roles=GlobalConstants.ModeratorRole)]
    public abstract class ModeratorController : BaseController
    {
    }
}