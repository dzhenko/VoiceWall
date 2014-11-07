namespace VoiceWall.CloudStorage.Common
{
    using System.IO;

    public interface ICloudStorage
    {
        /// <summary>
        /// Uploads a file as a base 64 string with givven name and returns its download url.
        /// </summary>
        /// <param name="stream">The file stream to be uploaded.</param>
        /// <param name="filename">The filename that will be saved.</param>
        /// <param name="filetype">The type of the file that will be saved.</param>
        /// <param name="path">Optional path for the location on the cloud storage.</param>
        /// <returns>The uploaded file url.</returns>
        string UploadFile(string base64, string filename, string filetype, string path = "/");

        /// <summary>
        /// Uploads a file as a byte array with givven name and returns its download url.
        /// </summary>
        /// <param name="stream">The file stream to be uploaded.</param>
        /// <param name="filename">The filename that will be saved.</param>
        /// <param name="filetype">The type of the file that will be saved.</param>
        /// <param name="path">Optional path for the location on the cloud storage.</param>
        /// <returns>The uploaded file url.</returns>
        string UploadFile(byte[] bytes, string filename, string filetype, string path = "/");

        /// <summary>
        /// Uploads a file as a stream with givven name and returns its download url.
        /// </summary>
        /// <param name="stream">The file stream to be uploaded.</param>
        /// <param name="filename">The filename that will be saved.</param>
        /// <param name="filetype">The type of the file that will be saved.</param>
        /// <param name="path">Optional path for the location on the cloud storage.</param>
        /// <returns>The uploaded file url.</returns>
        string UploadFile(Stream stream, string filename, string filetype, string path = "/");

        /// <summary>
        /// Deletes a file by its name.
        /// </summary>
        /// <param name="filename">The name of the file to be deleted.</param>
        /// <returns>If the operation has succeeded.</returns>
        bool DeleteFile(string filename);
    }

}
