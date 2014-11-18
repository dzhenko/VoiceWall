namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Moderation;

    [TestClass]
    public class ContentModerationServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new ContentModerationService(null);
        }
    }
}
