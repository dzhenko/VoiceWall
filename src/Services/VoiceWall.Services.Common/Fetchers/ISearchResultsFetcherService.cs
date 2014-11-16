namespace VoiceWall.Services.Common.Fetchers
{
    using System.Linq;

    using VoiceWall.Data.Models;

    public interface ISearchResultsFetcherService
    {
        IQueryable<User> SearchUsers(string search);

        IQueryable<Content> SearchAll(string search);

        IQueryable<Content> SearchVideos(string search);

        IQueryable<Content> SearchVoices(string search);

        IQueryable<Content> SearchPictures(string search);
    }
}
