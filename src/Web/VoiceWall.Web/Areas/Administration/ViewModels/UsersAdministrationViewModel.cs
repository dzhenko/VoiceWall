namespace VoiceWall.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using VoiceWall.Data.Models;
    using VoiceWall.Web.Infrastructure.Mapping;

    public class UsersAdministrationViewModel : IMapFrom<User>, IMapCustom
    {
        public bool IsHidden { get; set; }
        
        public string Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Image")]
        public string UserImage { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Has Admin Rights")]
        public bool IsCurrentlyAdmin { get; set; }

        [Display(Name = "Has Moderator Rights")]
        public bool IsCurrentlyModerator { get; set; }
        
        [Display(Name = "Total Contents Created")]
        public int? ContentsCreatedCount { get; set; }

        [Display(Name = "Total Comments Created")]
        public int? CommentsCreatedCount { get; set; }

        [Display(Name = "Total Contents Views Count")]
        public int? ContentViewsCount { get; set; }

        [Display(Name = "Total Comments Views Count")]
        public int? CommentViewsCount { get; set; }

        [Display(Name = "Total Content Likes Givven")]
        public int? ContentLikesGivvenCount { get; set; }

        [Display(Name = "Total Content Hates Givven")]
        public int? ContentHatesGivvenCount { get; set; }

        [Display(Name = "Total Content Flags Givven")]
        public int? ContentFlagsGivvenCount { get; set; }

        [Display(Name = "Total Comment Flags Givven")]
        public int? CommentFlagsGivvenCount { get; set; }

        [Display(Name = "Total Content Likes Recieved")]
        public int? ContentLikesRecievedCount { get; set; }

        [Display(Name = "Total Content Hates Recieved")]
        public int? ContentHatesRecievedCount { get; set; }

        [Display(Name = "Total Content Flags Recieved")]
        public int? ContentFlagsRecievedCount { get; set; }

        [Display(Name = "Total Comment Flags Recieved")]
        public int? CommentFlagsRecievedCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UsersAdministrationViewModel>()
                .ForMember(m => m.IsCurrentlyAdmin, opt => opt.MapFrom(u => u.Roles.Count() >= 3))
                .ForMember(m => m.IsCurrentlyModerator, opt => opt.MapFrom(u => u.Roles.Count() >= 2))
                .ForMember(m => m.ContentsCreatedCount, opt => opt.MapFrom(u => u.Contents.Count()))
                .ForMember(m => m.CommentsCreatedCount, opt => opt.MapFrom(u => u.Comments.Count()))
                .ForMember(m => m.ContentViewsCount, opt => opt.MapFrom(u => u.Comments.Count()))
                .ForMember(m => m.CommentViewsCount, opt => opt.MapFrom(u => u.Comments.Count()))
                .ForMember(m => m.ContentLikesGivvenCount, opt => opt.MapFrom(u => u.ContentViews.Count(v => v.Liked == true)))
                .ForMember(m => m.ContentHatesGivvenCount, opt => opt.MapFrom(u => u.ContentViews.Count(v => v.Liked == false)))
                .ForMember(m => m.ContentFlagsGivvenCount, opt => opt.MapFrom(u => u.ContentViews.Count(v => v.Flagged == true)))
                .ForMember(m => m.CommentFlagsGivvenCount, opt => opt.MapFrom(u => u.CommentViews.Count(v => v.Flagged == true)))
                .ForMember(m => m.ContentLikesRecievedCount, opt => opt.MapFrom(u => u.Contents.Sum(c => c.ContentViews.Count(v => v.Liked == true))))
                .ForMember(m => m.ContentHatesRecievedCount, opt => opt.MapFrom(u => u.Contents.Sum(c => c.ContentViews.Count(v => v.Liked == false))))
                .ForMember(m => m.ContentFlagsRecievedCount, opt => opt.MapFrom(u => u.Contents.Sum(c => c.ContentViews.Count(v => v.Flagged == true))))
                .ForMember(m => m.CommentFlagsRecievedCount, opt => opt.MapFrom(u => u.Comments.Sum(c => c.CommentViews.Count(v => v.Flagged == true))));
        }
    }
}