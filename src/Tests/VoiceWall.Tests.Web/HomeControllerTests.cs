namespace VoiceWall.Tests.Web.Moderation
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Controllers;

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new HomeController(null);
        }
    }
}