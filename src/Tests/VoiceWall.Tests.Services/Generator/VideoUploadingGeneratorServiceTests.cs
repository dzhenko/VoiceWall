namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Generator;

    [TestClass]
    public class VideoUploadingGeneratorServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new VideoUploadingGeneratorService(null, null);
        }
    }
}
