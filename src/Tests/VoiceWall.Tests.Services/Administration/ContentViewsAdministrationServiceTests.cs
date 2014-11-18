namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Administration;

    [TestClass]
    public class ContentViewsAdministrationServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new ContentViewsAdministrationService(null);
        }
    }
}
