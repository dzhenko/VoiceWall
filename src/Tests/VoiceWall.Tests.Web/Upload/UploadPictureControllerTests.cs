namespace VoiceWall.Tests.Web.Moderation
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Controllers;

    [TestClass]
    public class UploadPictureControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new UploadPictureController(null, null);
        }
    }
}