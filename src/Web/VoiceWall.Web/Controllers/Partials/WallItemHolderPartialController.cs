﻿namespace VoiceWall.Web.Controllers.Partials
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;
    using VoiceWall.Web.ViewModels;

    public class WallItemHolderPartialController : BaseController
    {
        private const string PartialViewName = "_WallItemHolderPartial";

        private readonly IContentFetcherService contentFetcherService;

        public WallItemHolderPartialController(IContentFetcherService contentFetcherService)
        {
            this.contentFetcherService = contentFetcherService;
        }

        [ChildActionOnly]
        public ActionResult GetFromId(Guid id)
        {
            return this.GetFromQueryable(this.contentFetcherService.GetById(id));
        }

        [ChildActionOnly]
        public ActionResult GetFromQueryable(IQueryable<Content> queryable)
        {
            var model = queryable.Project().To<WallItemHolderViewModel>().FirstOrDefault();
            var state = this.contentFetcherService.ContentLikedFlaggedByUser(Guid.Parse(model.Id), this.HttpContext.User.Identity.GetUserId());
            model.IsLiked = state == null ? null : state.IsLiked;
            model.IsFlagged = state == null ? false : state.IsFlagged;

            return this.GetFromViewModel(model);
        }

        [ChildActionOnly]
        public ActionResult GetFromViewModel(WallItemHolderViewModel viewModel)
        {
            return this.PartialView(PartialViewName, viewModel);
        }
    }
}