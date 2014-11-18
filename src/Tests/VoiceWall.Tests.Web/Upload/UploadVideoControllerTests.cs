namespace VoiceWall.Tests.Web.Moderation
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Controllers;

    [TestClass]
    public class UploadVideoControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new UploadVideoController(null, null);
        }
    }
}