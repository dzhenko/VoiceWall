namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Generator;

    [TestClass]
    public class PictureUploadingGeneratorServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new PictureUploadingGeneratorService(null, null);
        }
    }
}