namespace VoiceWall.Services.Common.Users
{
    using System.IO;

    public class UserUpdateProfileModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Stream UserImage { get; set; }

        public string UserImageMimeType { get; set; }
    }
}
