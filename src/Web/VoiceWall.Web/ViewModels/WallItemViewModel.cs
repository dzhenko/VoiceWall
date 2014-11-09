namespace VoiceWall.Web.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VoiceWall.Web.Infrastructure.Mapping;
    using VoiceWall.Data.Models;

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
                .ForMember(m => m.Comments, opt => opt.MapFrom(content => 
                    content.Comments.Select(c => new WallItemCommentViewModel()
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
        }
    }
}