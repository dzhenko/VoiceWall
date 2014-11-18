namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Generator;

    [TestClass]
    public class SoundUploadingGeneratorServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new SoundUploadingGeneratorService(null, null);
        }
    }
}
