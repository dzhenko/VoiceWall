namespace VoiceWall.CloudStorage.Dropbox
{
    using System;
    using System.IO;

    using DropNet;

    using VoiceWall.Common.ExtensionMethods;
    using VoiceWall.CloudStorage.Common;

    public class DropboxCloudStorage : ICloudStorage, IPicturesCloudStorage, IVideosCloudStorage, ISoundsCloudStorage, IUserProfilePicturesCloudStorage
    {
        private const string DropboxAppKey = "jkgqqefhutacq4n";
        private const string DropboxAppSecret = "21kf0wc1d5kn38j";
        private const string OauthAccessTokenValue = "ra72amhofx2x56vp";
        private const string OauthAccessTokenSecret = "cf87kxku32j17mg";

        private readonly DropNetClient client;

        public DropboxCloudStorage()
        {
            this.client = new DropNetClient(DropboxAppKey, DropboxAppSecret, OauthAccessTokenValue, OauthAccessTokenSecret);
        }

        public string UploadFile(Stream stream, string filename, string filetype, string path = "/")
        {
            if (stream == null || !stream.CanRead)
            {
                throw new ArgumentException("stream");
            }

            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("filename");
            }

            if (string.IsNullOrEmpty(filetype))
            {
                throw new ArgumentException("filetype");
            }

            var fullFileName = filename + filetype.GetFileExtension();

            this.client.UploadFile(path, fullFileName, stream);
            var meta = client.GetMedia(fullFileName);
            return meta.Url;
        }

        public string UploadFile(byte[] bytes, string filename, string filetype, string path = "/")
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("bytes");
            }

            return this.UploadFile(new MemoryStream(bytes), filename, filetype, path);
        }

        public string UploadFile(string base64, string filename, string filetype, string path = "/")
        {
            if (string.IsNullOrEmpty(base64))
            {
                throw new ArgumentException("base 64");
            }

            return this.UploadFile(Convert.FromBase64String(base64), filename, filetype, path);
        }

        public bool DeleteFile(string filename)
        {
            var meta = this.client.Delete(filename);
            return meta.Is_Deleted;
        }
    }
}
