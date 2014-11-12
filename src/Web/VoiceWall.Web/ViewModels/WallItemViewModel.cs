namespace VoiceWall.Web.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;

    using VoiceWall.Web.Infrastructure.Mapping;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;

    public class WallItemViewModel : IMapCustom
    {
        public WallItemHolderViewModel WallItemHolderViewModel { get; set; }

        public IEnumerable<WallItemCommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Content, WallItemViewModel>()
                .ForMember(m => m.WallItemHolderViewModel, opt => opt.MapFrom(c => new WallItemHolderViewModel() 
                {
                    ContentType = c.ContentType,
                    ContentUrl = c.ContentUrl,
                    CreatedOn = c.CreatedOn,
                    Flags = c.ContentViews.Count(v => v.Flagged == true),
                    Hates = c.ContentViews.Count(v => v.Liked == false),
                    Likes = c.ContentViews.Count(v => v.Liked == true),
                    Id = c.Id.ToString(),
                    UserId = c.UserId,
                    UserImage = c.User.UserImage,
                    UserName = c.User.FirstName + " " + c.User.LastName,
                }));

            configuration.CreateMap<Content, WallItemViewModel>()
                .ForMember(m => m.Comments, opt => opt.MapFrom(content => content.Comments.AsQueryable()
                    .OrderByDescending(c => c.CreatedOn)
                    .Select(c => new WallItemCommentViewModel()
                    {
                        ContentType = c.ContentType,
                        ContentUrl = c.ContentUrl,
                        CreatedOn = c.CreatedOn,
                        Flags = c.CommentViews.Count(v => v.Flagged == true),
                        Id = c.Id.ToString(),
                        UserId = c.UserId,
                        UserImage = c.User.UserImage,
                        UserName = c.User.FirstName + " " + c.User.LastName
                    })));

            configuration.CreateMap<AnalyzedContentQuery, WallItemViewModel>()
                .ForMember(m => m.WallItemHolderViewModel, opt => opt.MapFrom(c => new WallItemHolderViewModel()
                {
                    ContentType = c.OriginalContent.ContentType,
                    ContentUrl = c.OriginalContent.ContentUrl,
                    CreatedOn = c.OriginalContent.CreatedOn,
                    Flags = c.OriginalContent.ContentViews.Count(v => v.Flagged == true),
                    Hates = c.OriginalContent.ContentViews.Count(v => v.Liked == false),
                    Likes = c.OriginalContent.ContentViews.Count(v => v.Liked == true),
                    Id = c.OriginalContent.Id.ToString(),
                    UserId = c.OriginalContent.UserId,
                    UserImage = c.OriginalContent.User.UserImage,
                    UserName = c.OriginalContent.User.FirstName + " " + c.OriginalContent.User.LastName,
                }))
                .AfterMap((a,c) => 
                {
                    if (a.ContentStateForUser != null)
                    {
                        c.WallItemHolderViewModel.IsLiked = a.ContentStateForUser.IsLiked;
                        c.WallItemHolderViewModel.IsFlagged = a.ContentStateForUser.IsFlagged;
                    }
                });

            configuration.CreateMap<AnalyzedContentQuery, WallItemViewModel>()
                .ForMember(m => m.Comments, opt => opt.MapFrom(content => content.OriginalContent.Comments.AsQueryable()
                    .OrderByDescending(c => c.CreatedOn)
                    .Select(c => new WallItemCommentViewModel()
                    {
                        ContentType = c.ContentType,
                        ContentUrl = c.ContentUrl,
                        CreatedOn = c.CreatedOn,
                        Flags = c.CommentViews.Count(v => v.Flagged == true),
                        Id = c.Id.ToString(),
                        UserId = c.UserId,
                        UserImage = c.User.UserImage,
                        UserName = c.User.FirstName + " " + c.User.LastName
                    })))
                .AfterMap((a, c) => 
                {
                    foreach (var commentInfo in a.ContentCommentsFlags)
                    {
                        if (commentInfo != null)
                        {
                            var comment = c.Comments.FirstOrDefault(com => com.Id == commentInfo.CommentId.ToString());
                            if (comment != null)
                            {
                                comment.IsFlagged = commentInfo.IsFlagged;
                            }
                        }
                    }
                });


        }
    }
}