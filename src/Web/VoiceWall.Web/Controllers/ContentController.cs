//namespace VoiceWall.Web.Controllers
//{
//    using System;
//    using System.Linq;
//    using System.Web.Mvc;

//    using AutoMapper.QueryableExtensions;
//    using Microsoft.AspNet.Identity;

//    using VoiceWall.CloudStorage.Common;
//    using VoiceWall.Data.Common.Repositories;
//    using VoiceWall.Data.Models;
//    using VoiceWall.Web.Infrastructure.Filters;
//    using VoiceWall.Web.ViewModels.Upload;
//    using VoiceWall.Web.ViewModels;

    // TODO: Clean this
    //// main idea is to return WallItem, WallItemHolder and WallItemComment with this class. It will be
    //// called only as child action and will return these partials after interactions
    //// maybe to create new item

    //[ChildActionOnly]
    //public class ContentController : Controller
    //{
    //    private readonly IRepository<Content> contentsRepository;
    //    private readonly IRepository<Comment> commentsRepository;

    //    public ContentController (IRepository<Content> contentsRepository, IRepository<Comment> commentsRepository)
    //    {
    //        this.contentsRepository = contentsRepository;
    //        this.commentsRepository = commentsRepository;
    //    }

    //    public ActionResult WallItemComment(Guid commentId)
    //    {
    //        var viewModel = this.commentsRepository.All()
    //                            .Where(c => c.Id == commentId)
    //                            .Project()
    //                            .To<WallItemCommentViewModel>()
    //                            .FirstOrDefault();

    //        return this.PartialView("_WallItemCommentPartial", viewModel);
    //    }
    //}
//}