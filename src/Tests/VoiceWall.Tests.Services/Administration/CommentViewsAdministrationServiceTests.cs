namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Administration;

    [TestClass]
    public class CommentViewsAdministrationServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new CommentViewsAdministrationService(null);
        }
    }
}
