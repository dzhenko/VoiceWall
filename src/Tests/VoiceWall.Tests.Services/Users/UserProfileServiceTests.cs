namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Users;

    [TestClass]
    public class UserProfileServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new UserProfileService(null, null);
        }
    }
}
