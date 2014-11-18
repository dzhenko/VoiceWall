namespace VoiceWall.Tests.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VoiceWall.Web.Controllers;

    [TestClass]
    public class StaticPagesControllerTests
    {
        [TestMethod]
        public void InstancingTheControllerShouldNotThrowException()
        {
            var controller = new StaticPagesController();
        }

        [TestMethod]
        public void CallingControllerAboutViewShouldNotThrowException()
        {
            var controller = new StaticPagesController();
            controller.About();
        }

        [TestMethod]
        public void CallingControllerFAQViewShouldNotThrowException()
        {
            var controller = new StaticPagesController();
            controller.FAQ();
        }

        [TestMethod]
        public void CallingControllerContactViewShouldNotThrowException()
        {
            var controller = new StaticPagesController();
            controller.Contact();
        }

        [TestMethod]
        public void CallingControllerHomeViewShouldNotThrowException()
        {
            var controller = new StaticPagesController();
            controller.Home();
        }

        [TestMethod]
        public void CallingControllerHomeViewShouldReturnPernamentRedirect()
        {
            var controller = new StaticPagesController();
            var result = controller.Home() as RedirectToRouteResult;
            Assert.IsTrue(result.Permanent);
        }
    }
}
