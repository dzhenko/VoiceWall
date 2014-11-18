namespace VoiceWall.Services.Administration
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Administration;
    using VoiceWall.Common;

    public class UsersAdministrationService : BaseAdministrationService, IUserAdministrationService
    {
        public UsersAdministrationService(IVoiceWallData data)
            : base(data)
        {
        }

        public IEnumerable<User> Read()
        {
            return this.Data.Users.All();
        }

        public IEnumerable<Guid> UpdateUser(string userId, bool IsAdmin, bool IsModerator, bool IsHidden)
        {
            var allIds = this.GetContentIdsConnectedToUser(userId, IsHidden, false);

            var user = this.Data.Users.GetById(userId);

            var userManager = new UserManager<User>(new UserStore<User>(this.Data.Context));

            user.IsHidden = IsHidden;

            if (IsAdmin)
            {
                userManager.AddToRole(userId, GlobalConstants.AdminRole);
            }
            else
            {
                userManager.RemoveFromRole(userId, GlobalConstants.AdminRole);
            }

            if (IsModerator)
            {
                userManager.AddToRole(userId, GlobalConstants.ModeratorRole);
            }
            else
            {
                userManager.RemoveFromRole(userId, GlobalConstants.ModeratorRole);
            }

            this.Data.Users.Update(user);
            this.Data.SaveChanges();

            return allIds;
        }

        public IEnumerable<Guid> DeleteUser(string userId)
        {
            var allIds = this.GetContentIdsConnectedToUser(userId, null, true);

            this.Data.Users.Delete(userId);
            this.Data.SaveChanges();

            return allIds;
        }

        private IEnumerable<Guid> GetContentIdsConnectedToUser(string userId, bool? hide, bool delete)
        {
            var allIdsToReturn = this.Data.Users
                .All()
                .Where(u => u.Id == userId)
                .Select(u => new 
                {
                    ContentsIds = u.Contents.Select(c => c.Id),
                    CommentsIds = u.Comments.Select(c => c.ContentId),
                    ContentsViewsIds = u.ContentViews.Select(cv => cv.ContentId),
                    CommentViewsIds = u.CommentViews.Select(cv => cv.Comment.ContentId)
                })
                .FirstOrDefault();

            var allIds = this.Data.Users
                .All()
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    ContentsIds = u.Contents.Select(c => c.Id),
                    CommentsIds = u.Comments.Select(c => c.Id),
                    ContentsViewsIds = u.ContentViews.Select(cv => cv.Id),
                    CommentViewsIds = u.CommentViews.Select(cv => cv.Id)
                })
                .FirstOrDefault();

            var itemsToReturn = new HashSet<Guid>(allIdsToReturn
                .ContentsIds
                .Union(allIds.CommentsIds)
                .Union(allIds.ContentsViewsIds)
                .Union(allIds.CommentViewsIds));

            if (hide == null && !delete)
	        {
		        return itemsToReturn;
	        }

            foreach (var content in allIds.ContentsIds)
            {
                if (hide.HasValue)
                {
                    var contentObj = this.Data.Contents.GetById(content);
                    contentObj.IsHidden = hide.Value;
                    this.Data.Contents.Update(contentObj);
                }
                else
                {
                    this.Data.Contents.Delete(content);
                }
            }

            this.Data.SaveChanges();

            foreach (var comment in allIds.CommentsIds)
            {
                if (hide.HasValue)
                {
                    var commentObj = this.Data.Comments.GetById(comment);
                    commentObj.IsHidden = hide.Value;
                    this.Data.Comments.Update(commentObj);
                }
                else
                {
                    this.Data.Comments.Delete(comment);
                }
            }

            this.Data.SaveChanges();

            foreach (var contentView in allIds.ContentsViewsIds)
            {
                if (hide.HasValue)
                {
                    var contentViewObj = this.Data.ContentViews.GetById(contentView);
                    contentViewObj.IsHidden = hide.Value;
                    this.Data.ContentViews.Update(contentViewObj);
                }
                else
                {
                    this.Data.ContentViews.Delete(contentView);
                }
            }

            this.Data.SaveChanges();

            foreach (var commentView in allIds.CommentViewsIds)
            {
                if (hide.HasValue)
                {
                    var commentViewObj = this.Data.CommentViews.GetById(commentView);
                    commentViewObj.IsHidden = hide.Value;
                    this.Data.CommentViews.Update(commentViewObj);
                }
                else
                {
                    this.Data.CommentViews.Delete(commentView);
                }
            }

            this.Data.SaveChanges();

            return itemsToReturn;
        }
    }
}
