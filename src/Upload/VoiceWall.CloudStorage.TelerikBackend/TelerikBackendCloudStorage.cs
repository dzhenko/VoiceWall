namespace VoiceWall.CloudStorage.TelerikBackend
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    using Telerik;
    using Telerik.Everlive.Sdk.Core;
    using Telerik.Everlive.Sdk.Core.Query.Definition.FormData;

    using VoiceWall.Common.ExtensionMethods;
    using VoiceWall.CloudStorage.Common;

    public class TelerikBackendCloudStorage : ICloudStorage, IPicturesCloudStorage, IVideosCloudStorage, ISoundsCloudStorage, IUserProfilePicturesCloudStorage
    {
        private const string EverliveAppKey = "lYI6vh7P7BFSL2Wr";

        private readonly EverliveApp app;

        public TelerikBackendCloudStorage()
        {
            this.app = new EverliveApp(EverliveAppKey);
        }

        public string UploadFile(string base64, string filename, string filetype, string path = "/")
        {
            if (string.IsNullOrEmpty(base64))
            {
                throw new ArgumentException("base 64");
            }

            return this.UploadFile(Convert.FromBase64String(base64), filename, filetype, path);
        }

        public string UploadFile(byte[] bytes, string filename, string filetype, string path = "/")
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("bytes");
            }

            return this.UploadFile(new MemoryStream(bytes), filename, filetype, path);
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

            var uploadResult = this.app.WorkWith().Files()
                .Upload(new FileField("fieldName", filename + filetype.GetFileExtension(), filetype, stream)).ExecuteSync();

            return app.WorkWith().Files().GetById(uploadResult.Id).ExecuteSync().CustomProperties["Uri"].ToString();
        }

        public bool DeleteFile(string filename)
        {
            var files = this.app.WorkWith().Files().Delete().Where(f => f.Filename == filename).ExecuteSync();
            return files > 0;
        }
    }
}

//  public List<Tuple<string, string>> GetUploadedFiles()
//  {
//      var allFiles = app.WorkWith().Files().GetAll().ExecuteSync();
        
//      var list = new List<Tuple<string, string>>();
        
//      foreach (var file in allFiles)
//      {
//          list.Add(new Tuple<string, string>(file.Filename, app.WorkWith().Files().GetFileDownloadUrl(file.Id)));
//      }
        
//      return list;
//  }