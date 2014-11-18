namespace VoiceWall.Tests.Web.Moderation
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Areas.Administration.Controllers;

    [TestClass]
    public class CommentsAdministrationControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new CommentsAdministrationController(null, null);
        }
    }
}

