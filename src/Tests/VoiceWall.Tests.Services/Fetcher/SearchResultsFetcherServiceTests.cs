namespace VoiceWall.Tests.Services.Users
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Services.Fetcher;

    [TestClass]
    public class SearchResultsFetcherServiceTests
    {
        [TestMethod]
        public void InstancingTheServiceShouldNotCauseException()
        {
            new SearchResultsFetcherService(null);
        }
    }
}
