﻿namespace VoiceWall.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Wall", ViewModels.FakeDataSeeder.GetWallItems(15));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVideoAndAudio(HttpPostedFileBase videoAndAudioFile, string wallItemId /*create input model ?*/)
        {
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVideo(HttpPostedFileBase videoFile, string wallItemId /*create input model ?*/)
        {
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        public ActionResult Voice()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadVoice(HttpPostedFileBase waveBlobFile, string wallItemId /*create input model ?*/)
        {
            // magic
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }

        public ActionResult Picture()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadPicture(HttpPostedFileBase imageFile, string wallItemId /*create input model ?*/)
        {
            // magic
            return Json(JsonModels.SuccessJsonModel.Succeeded);
        }
    }
}