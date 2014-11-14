namespace VoiceWall.Services.Users
{
    using System.IO;
    using System.Linq;
    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Users;

    public class OwnProfileService : IOwnProfileService
    {
        private IVoiceWallData data;
        private IUserProfilePicturesCloudStorage storage;

        public OwnProfileService(IVoiceWallData data, IUserProfilePicturesCloudStorage storage)
        {
            this.data = data;
            this.storage = storage;
        }

        public IQueryable<User> GetUserProfile(string userId)
        {
            return this.data.Users.All().Where(u => u.Id == userId);
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
    }
}
