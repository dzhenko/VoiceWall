namespace VoiceWall.Services.Users
{
    using System.IO;
    using System.Linq;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Users;

    public class UserProfileService : IUserProfileService
    {
        private IVoiceWallData data;
        private IUserProfilePicturesCloudStorage storage;

        public UserProfileService(IVoiceWallData data, IUserProfilePicturesCloudStorage storage)
        {
            this.data = data;
            this.storage = storage;
        }

        public IQueryable<User> GetUserProfile(string userId)
        {
            return this.data.Users.All().Where(u => u.Id == userId && !u.IsHidden);
        }

        public string UpdateUserProfile(string userId, UserUpdateProfileModel model)
        {
            var user = this.data.Users.GetById(userId);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var imageUrl = storage.UploadFile(model.UserImage, user.Id, model.UserImageMimeType, "Profiles/");

            user.UserImage = imageUrl;

            this.data.Users.Update(user);
            this.data.SaveChanges();

            return user.Id;
        }


        public string ChangeUserPicture(string userId, Stream stream, string mimeType)
        {
            var url = this.storage.UploadFile(stream, userId, mimeType);

            var user = this.data.Users.GetById(userId);
            user.UserImage = url;
            this.data.Users.Update(user);
            this.data.SaveChanges();

            return userId;
        }

        public IQueryable<Content> GetNext(string userId, int skip = 5, int count = 5)
        {
            return this.data.Contents.All().Where(c => !c.IsHidden && c.UserId == userId)
                .OrderByDescending(c => c.CreatedOn).Skip(skip).Take(count);
        }
    }
}
