﻿namespace VoiceWall.Tests.Web.Moderation
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Areas.Fun.Controllers;

    [TestClass]
    public class GamesControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new GamesController();
        }
    }
}
