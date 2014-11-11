namespace VoiceWall.Web.Controllers
{
    using System;
using System.Web.Mvc;
using VoiceWall.Data;
using VoiceWall.Data.Common.Repositories;
using VoiceWall.Data.Models;

    /// <summary>
    /// Abstract controller used to provide uow to its successors.
    /// </summary>

    public abstract class BaseController : Controller
    {
        private readonly IVoiceWallData data;

        public BaseController(IVoiceWallData data)
        {
            this.data = data;
        }

        protected IVoiceWallData Data
        {
            get { return this.data; }
        }

        protected ActionResult ConditionalActionResult(Action actionToPerform, ActionResult resultToReturn)
        {
            try
            {
                actionToPerform();
            }
            catch (Exception ex)
            {
                return this.HttpNotFound(ex.Message);
            }

            return resultToReturn;
        }
    }
}