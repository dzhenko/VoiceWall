namespace VoiceWall.Tests.Web.Moderation
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Areas.Administration.Controllers;

    [TestClass]
    public class CommentViewsAdministrationControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new CommentViewsAdministrationController(null);
        }
    }
}
